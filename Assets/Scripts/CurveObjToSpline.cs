using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Splines;

public class CurveObjToSpline : MonoBehaviour
{
    public string CurveObjTextPath;
    public SplineContainer GeneratedSpline;
    public List<Vector3> SplineVectors = new();

    public void UpdateSpline()
    {
        SplineVectors.Clear();
        StreamReader reader = new StreamReader(CurveObjTextPath);
        char currentLineFirstChar = ' ';
        string currLine = "";

        //find start
        do
        {
            currLine = reader.ReadLine();

            if (reader.EndOfStream)
                break;

            currentLineFirstChar = currLine[0];

        } while (currentLineFirstChar != 'o');

        List<string> txtSplineVectors = new();

        do
        {
            currLine = reader.ReadLine();
            currentLineFirstChar = currLine[0];

            currLine = currLine.Substring(2);

            txtSplineVectors.Add(currLine);

            if (reader.EndOfStream)
                break;

        } while (currentLineFirstChar != 'l');


        foreach (string vector in txtSplineVectors)
        {
            string[] expanded = vector.Split(' ');

            if (expanded.Length != 3)
                break;

            float x = float.Parse(expanded[0]);
            float y = float.Parse(expanded[1]);
            float z = float.Parse(expanded[2]);

            Vector3 newVector = new(-x, y, z);

            SplineVectors.Add(newVector);
        }

        reader.Close();

        GeneratedSpline.Spline.Resize(0);
        SplineVectors.Reverse();

        foreach (Vector3 vector in SplineVectors)
        {
            BezierKnot newKnot = new();

            newKnot.Position = new(vector.x, vector.y, vector.z);

            GeneratedSpline.Spline.Add(newKnot);
        }

    }
}
