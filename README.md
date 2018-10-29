# ProceduralMesh

Procedural Mesh for Unity

## Plane

<img src="https://user-images.githubusercontent.com/10070637/47652176-a00dc800-dbc8-11e8-979d-d21a0eafc6dd.png" width=50%>

add `Plane Mesh Creator` component or add a script below to empty GameObject

```csharp
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralMeshScript : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = ProceduralMesh.Plane(
            new Vector2(4, 3), // size
            new Vector2Int(4, 3) // segments
        );
    }
}
```

##  Box

<img src="https://user-images.githubusercontent.com/10070637/47652181-a3a14f00-dbc8-11e8-9b94-453de9ed47ca.png" width=50%>

add `Plane Box Creator` component or add a script below to empty GameObject

```csharp
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralMeshScript : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = ProceduralMesh.Box(
            new Vector3(2f, 1.5f, 1f), // size
            new Vector3Int(4, 3, 2) // segments
        );
    }
}
```

## Sphere

<img src="https://user-images.githubusercontent.com/10070637/47652183-a603a900-dbc8-11e8-8e32-1b3bdd9739e5.png" width=50%>

add `Plane Sphere Creator` component or add a script below to empty GameObject

```csharp
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralMeshScript : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = ProceduralMesh.Sphere(
            1.0f, // radius
            8, // thetaSegments
            12 // phiSegments
        );
    }
}
```

## Icosphere

<img src="https://user-images.githubusercontent.com/10070637/47652189-a7cd6c80-dbc8-11e8-8c9a-23c1a83d99f4.png" width=50%>

add `Plane Icosphere Creator` component or add a script below to empty GameObject

```csharp
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralMeshScript : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = ProceduralMesh.Icosphere(
            1, // radius
            1 // divisions
        );
    }
}
```

## Torus

<img src="https://user-images.githubusercontent.com/10070637/47652197-ab60f380-dbc8-11e8-8fd0-7c09266fac9a.png" width=50%>

add `Plane Torus Creator` component or add a script below to empty GameObject

```csharp
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralMeshScript : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = ProceduralMesh.Torus(
            3f, // majorRadius
            1f, // minorRadius
            12, // thetaSegment
            8 // phiSegment
        );
    }
}
```

## Cylinder

<img src="https://user-images.githubusercontent.com/10070637/47652200-ad2ab700-dbc8-11e8-9865-fb55d2de81a4.png" width=50%>

add `Plane Cylinder Creator` component or add a script below to empty GameObject

```csharp
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralMeshScript : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = ProceduralMesh.Cylinder(
            2.0f, // height
            1.0f, // radius
            5, // heightSegments
            8 // angleSegments
        );
    }
}
```

## Inflated Plane

<img src="https://user-images.githubusercontent.com/10070637/47652207-b025a780-dbc8-11e8-9458-e59754a728fd.png" width=50%>

add `Plane Inflated Plane Creator` component or add a script below to empty GameObject

```csharp
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralMeshScript : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = ProceduralMesh.InflatedPlane(
            new Vector3(3.0f, 2.0f, 1.0f), // size
            2.0f, // exp
            new Vector2Int(15, 10) // segments
        );
    }
}
```
