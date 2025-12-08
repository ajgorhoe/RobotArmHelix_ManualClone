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

            // ---- 1. Add the top pole ----
            mesh.Positions.Add(new Point3D(center.X, center.Y, center.Z + radius));
            mesh.Normals.Add(new Vector3D(0, 0, 1));

            // ---- 2. Generate latitude rings (excluding poles) ----
            for (int pi = 1; pi < pDiv; pi++)
            {
                double phi = Math.PI * pi / pDiv;  // 0..PI

                for (int ti = 0; ti < tDiv; ti++)
                {
                    double theta = 2 * Math.PI * ti / tDiv;  // 0..2PI

                    double x = Math.Sin(phi) * Math.Cos(theta);
                    double y = Math.Sin(phi) * Math.Sin(theta);
                    double z = Math.Cos(phi);

                    Vector3D normal = new Vector3D(x, y, z);
                    normal.Normalize();

                    mesh.Positions.Add(new Point3D(
                        center.X + radius * x,
                        center.Y + radius * y,
                        center.Z + radius * z));

                    mesh.Normals.Add(normal);
                }
            }

            // ---- 3. Add the bottom pole ----
            mesh.Positions.Add(new Point3D(center.X, center.Y, center.Z - radius));
            mesh.Normals.Add(new Vector3D(0, 0, -1));

            int northPole = 0;
            int southPole = mesh.Positions.Count - 1;

            // ---- 4. Triangles for the top cap ----
            for (int ti = 0; ti < tDiv; ti++)
            {
                int a = northPole;
                int b = 1 + ti;
                int c = 1 + ((ti + 1) % tDiv);
                mesh.TriangleIndices.Add(a);
                mesh.TriangleIndices.Add(b);
                mesh.TriangleIndices.Add(c);
            }

            // ---- 5. Triangles for the middle bands ----
            for (int pi = 0; pi < pDiv - 2; pi++)
            {
                int row1 = 1 + pi * tDiv;
                int row2 = row1 + tDiv;

                for (int ti = 0; ti < tDiv; ti++)
                {
                    int a = row1 + ti;
                    int b = row1 + (ti + 1) % tDiv;
                    int c = row2 + ti;
                    int d = row2 + (ti + 1) % tDiv;

                    mesh.TriangleIndices.Add(a);
                    mesh.TriangleIndices.Add(c);
                    mesh.TriangleIndices.Add(b);

                    mesh.TriangleIndices.Add(b);
                    mesh.TriangleIndices.Add(c);
                    mesh.TriangleIndices.Add(d);
                }
            }

            // ---- 6. Triangles for the bottom cap ----
            int lastRingStart = southPole - tDiv;
            for (int ti = 0; ti < tDiv; ti++)
            {
                int a = lastRingStart + ti;
                int b = lastRingStart + (ti + 1) % tDiv;
                int c = southPole;
                mesh.TriangleIndices.Add(a);
                mesh.TriangleIndices.Add(c);
                mesh.TriangleIndices.Add(b);
            }

            // ---- 7. Create the material ----
            MaterialGroup material = new MaterialGroup();
            material.Children.Add(new DiffuseMaterial(new SolidColorBrush(color)));
            material.Children.Add(new SpecularMaterial(new SolidColorBrush(color), 80));

            return new GeometryModel3D(mesh, material);
        }

    }
}
