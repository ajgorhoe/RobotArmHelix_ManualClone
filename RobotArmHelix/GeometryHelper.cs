using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace RobotArmHelix
{
    public class GeometryHelper
    {

        public static GeometryModel3D CreateDebugSphere(
            Point3D center,
            double radius,
            int tDiv,
            int pDiv,
            Color color)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();

            // Generate vertices & normals
            for (int pi = 0; pi <= pDiv; pi++)
            {
                double phi = Math.PI * pi / pDiv;

                for (int ti = 0; ti <= tDiv; ti++)
                {
                    double theta = 2 * Math.PI * ti / tDiv;

                    double x = Math.Sin(phi) * Math.Cos(theta);
                    double y = Math.Sin(phi) * Math.Sin(theta);
                    double z = Math.Cos(phi);

                    // position
                    mesh.Positions.Add(new Point3D(
                        center.X + radius * x,
                        center.Y + radius * y,
                        center.Z + radius * z));

                    // normal (unit vector)
                    Vector3D normal = new Vector3D(x, y, z);
                    normal.Normalize();
                    mesh.Normals.Add(normal);
                }
            }

            // Triangles
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

            // Create material
            MaterialGroup material = new MaterialGroup();
            material.Children.Add(new DiffuseMaterial(new SolidColorBrush(color)));
            material.Children.Add(new SpecularMaterial(new SolidColorBrush(color), 80));

            return new GeometryModel3D(mesh, material);
        }

    }
}
