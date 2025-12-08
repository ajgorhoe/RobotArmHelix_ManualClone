using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace RobotArmHelix
{
    public class GeometryHelper
    {

        public static GeometryModel3D CreateDebugSphere(Point3D center, double radius, int tDiv, int pDiv)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();

            for (int pi = 0; pi <= pDiv; pi++)
            {
                double phi = Math.PI * pi / pDiv;
                for (int ti = 0; ti <= tDiv; ti++)
                {
                    double theta = 2 * Math.PI * ti / tDiv;

                    double x = center.X + radius * Math.Sin(phi) * Math.Cos(theta);
                    double y = center.Y + radius * Math.Sin(phi) * Math.Sin(theta);
                    double z = center.Z + radius * Math.Cos(phi);

                    mesh.Positions.Add(new Point3D(x, y, z));
                }
            }

            for (int pi = 0; pi < pDiv; pi++)
            {
                for (int ti = 0; ti < tDiv; ti++)
                {
                    int a = pi * (tDiv + 1) + ti;
                    int b = a + 1;
                    int c = a + (tDiv + 1);
                    int d = c + 1;

                    mesh.TriangleIndices.Add(a);
                    mesh.TriangleIndices.Add(c);
                    mesh.TriangleIndices.Add(b);

                    mesh.TriangleIndices.Add(b);
                    mesh.TriangleIndices.Add(c);
                    mesh.TriangleIndices.Add(d);
                }
            }

            return new GeometryModel3D(mesh, Materials.Brown);
        }

    }
}
