using System.Collections.Generic;
using UnityEngine;

public partial class ProceduralMesh
{
    public class Builder
    {
        private List<Operation> operations;

        public Builder() { }

        public Mesh Build()
        {
            Mesh mesh = new Mesh();

            foreach (Operation op in operations)
            {
                op.Apply(mesh);
            }

            return mesh;
        }

        public Builder Cylinder(float height, float radius, int heightSegments, int angleSegments)
        {
            operations.Add(new CreateCylinderOp(height, radius, heightSegments, angleSegments));

            return this;
        }

        public Builder RecalculateAll()
        {
            operations.Add(new RecalculateAllOp());
            return this;
        }

        private interface Operation
        {
            void Apply(Mesh mesh);
        }

        private abstract class CreateOp : Operation
        {
            public void Apply(Mesh mesh)
            {
                Mesh newMesh = CreateMesh();
                mesh.vertices = newMesh.vertices;
                mesh.triangles = newMesh.triangles;
            }

            public abstract Mesh CreateMesh();

        }

        private class CreateCylinderOp : CreateOp
        {
            private readonly float height;
            private readonly float radius;
            private readonly int heightSegments;
            private readonly int angleSegments;

            public CreateCylinderOp(float height, float radius, int heightSegments, int angleSegments)
            {
                this.height = height;
                this.radius = radius;
                this.heightSegments = heightSegments;
                this.angleSegments = angleSegments;
            }

            public override Mesh CreateMesh()
            {
                return ProceduralMesh.Cylinder(height, radius, heightSegments, angleSegments, NO_RECALCULATION_OPTION);
            }
        }

        private abstract class ModifyVertexOp : Operation
        {
            public void Apply(Mesh mesh)
            {

            }
        }

        private class TwistOp : Operation
        {
            public TwistOp()
            {

            }

            public void Apply(Mesh mesh)
            {
                throw new System.NotImplementedException();
            }
        }

        private class RecalculateAllOp : Operation
        {
            public void Apply(Mesh mesh)
            {
                mesh.RecalculateNormals();
                mesh.RecalculateTangents();
                mesh.RecalculateBounds();
            }
        }

    }
}
