using System;
using System.Globalization;
using UnityEngine;

public class Figura : MonoBehaviour
{
    //parametri shape
    public TextAsset offFile;
    public GameObject ancora;
    public GameObject ancoraFissa;
    public GameObject ancoraFake;
    public float size;
    public float numAncoreFisse = 0;
    public float numAncoreFake = 0;
    public Material mat;
    private int[,] TRIV;
    private int n;
    private int m;
    private Mesh mesh;
    private float[,] VERT;
    private Vector3[] VERT1;
    private Vector3[] VERTINIZ;
    private GameObject[] ancore;
    private Vector3 canvasPos;
    private int[] boundary;
    float[,] A;
    public GameObject menuVite;
    private bool attive = true;
    public GameObject menuSconfitta;
    public GameObject menu;
    public GameObject vittoria;
    public GameObject tutorial;
    [HideInInspector]
    public float[] eigenvalue;


    void Awake()
    {
        //genero mesh
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();

        mf.mesh = mesh;
        GetComponent<MeshRenderer>().material = mat;

        // per il formato della conversione
        NumberFormatInfo provider = new NumberFormatInfo();
        provider.NumberDecimalSeparator = ".";

        //converto in stringa il file off e lo splitto per le righe
        string offString = offFile.ToString();
        string[] lines = offString.Split(new[] { "\n" }, StringSplitOptions.None);

        //mi trovo n(numero vertici) e m(numero triangoli) nel file off letto
        n = 0;
        m = 0;
        string[] columnNM = lines[1].Split(new[] { " " }, StringSplitOptions.None);
        n = int.Parse(columnNM[0]);
        m = int.Parse(columnNM[1]);

        //inizializzo i vettori
        Vector3[] vertices = new Vector3[n];
        int[] tri = new int[m * 3];
        Vector3[] normals = new Vector3[n];

        //media per coordinate baricentriche
        float mediax = 0;
        float mediay = 0;
        string[] bound = lines[0].Split(new[] { " " }, StringSplitOptions.None);
        boundary = new int[bound.Length];
        for (int i = 0; i < bound.Length; i++)
        {
            boundary[i] = int.Parse(bound[i]);
        }

        //mi leggo i valori da assegnare ai vettori
        for (int i = 2; i < lines.Length - 1; i++)
        {
            string[] column = lines[i].Split(new[] { " " }, StringSplitOptions.None);
            if (i < n + 2)
            {
                // Vertices (inserire i vertici nel modo descritto sopra ma con i vertici della nostra figura)
                float x = ((float)Convert.ToDouble(column[0], provider)) * size;
                float y = ((float)Convert.ToDouble(column[1], provider)) * size;
                float z = ((float)Convert.ToDouble(column[2], provider)) * size;
                mediax = mediax + x;
                mediay = mediay + y;
                vertices[i - 2] = new Vector3(x, -y, z);

                // Normals (only if you want to display object in the game) 
                normals[i - 2] = Vector3.forward;
            }
            else
            {
                // Triangles (i triangoli della figura)
                tri[(i - (n + 2)) * 3] = int.Parse(column[1]);
                tri[((i - (n + 2)) * 3) + 1] = int.Parse(column[2]);

                tri[((i - (n + 2)) * 3) + 2] = int.Parse(column[3]);
            }
        }
        //passo a coordinate baricentriche
        mediax = mediax / n;
        mediay = mediay / n;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i][0] = vertices[i][0] - mediax;
            vertices[i][1] = vertices[i][1] + mediay;
        }

        mesh.vertices = vertices;
        mesh.triangles = tri;
        mesh.normals = normals;

        // UVs (how texture are displayed) uv lo implemento dopo perché mi servono i vertici di bordo
        //Vector2[] uvs = new Vector2[mesh.vertices.Length];
        //for (int i = 0; i < uvs.Length; i++)
        //{
        //    uvs[i] = new Vector2(vertices[i].x, vertices[i].y);
        //}
        //mesh.uv = uvs;
    }
    private void Start()
    {
        VERT = new float[n, 3];
        mesh = GetComponent<MeshFilter>().mesh;
        VERT1 = mesh.vertices;
        VERTINIZ = new Vector3[VERT1.GetLength(0)];
        for (int i = 0; i < n; i++)
        {
            VERT[i, 0] = mesh.vertices[i].x;
            VERT[i, 1] = mesh.vertices[i].y;
            VERT[i, 2] = mesh.vertices[i].z;
        }
        TRIV = new int[mesh.triangles.Length / 3, 3];
        int col = 0;
        int rig = 0;
        foreach (int i in mesh.triangles)
        {
            if (col == 3)
            {
                col = 0;
                rig++;
            }
            TRIV[rig, col] = i;
            col++;
        }
        A = CalcAdj(TRIV);
        ancore = new GameObject[boundary.Length];
        float[,] L = Sottrai(Eye(n), ProdottoRigheColonne(Diag(Denom(Sum(A), 1f)), A));
        float[,] left = Left(L, boundary);
        float[,] right = Right(boundary, VERT);
        float[,] VERT_is = Sistema(left, right);

        VERT = VERT_is;
        float[,] M = Masse(VERT, TRIV);
        float[,] S = Stiffness(VERT, TRIV);


        Accord.Math.Decompositions.GeneralizedEigenvalueDecomposition autov =
            new Accord.Math.Decompositions.GeneralizedEigenvalueDecomposition(ConvFvtoDv(S), ConvFvtoDv(M));
        float[,] autovalori = ConvDvtoFv(autov.DiagonalMatrix);
        eigenvalue = new float[autovalori.GetLength(0)];

        for (int i = 0; i < autovalori.GetLength(0); i++)
        {
            eigenvalue[i] = autovalori[i, i];
        }

        float temp;
        int flag;
        int nn = eigenvalue.Length;
        int p = eigenvalue.Length;
        do
        {
            flag = 0;
            for (int i = 0; i < (nn - 1); i++)
                if (eigenvalue[i] > eigenvalue[i + 1])
                {
                    temp = eigenvalue[i];
                    eigenvalue[i] = eigenvalue[i + 1];
                    eigenvalue[i + 1] = temp;
                    flag = 1;
                    p = i + 1;
                }
            nn = p;
        }
        while (flag == 1);
        //eigenvalue1 = new float[eigenvalue.Length - 1];
        //for (int i = 1; i < eigenvalue.Length; i++)
        //{
        //    eigenvalue1[i - 1] = eigenvalue[i];
        //    print(eigenvalue[i]);
        //}

        for (int i = 0; i < VERT_is.GetLength(0); i++)
        {
            VERT1[i].x = VERT_is[i, 0];
            VERT1[i].y = VERT_is[i, 1];
            VERT1[i].z = VERT_is[i, 2];
        }
        mesh.vertices = VERT1;
        //VERTINIZ = VERT1;
        Vector2[] uvs = new Vector2[mesh.vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(mesh.vertices[i].x, mesh.vertices[i].y);
        }
        mesh.uv = uvs;
        mesh.RecalculateBounds();
        for (int i = 0; i < boundary.Length; i++)
        {
            if (i < numAncoreFisse)
            {
                ancore[i] = Instantiate(ancoraFissa, new Vector3(VERT[boundary[i], 0], VERT[boundary[i], 1], 0.01f), transform.rotation) as GameObject;
                ancore[i].transform.SetParent(this.transform);
            }
            else if (i >= numAncoreFisse && i < numAncoreFisse + numAncoreFake)
            {
                ancore[i] = Instantiate(ancoraFake, new Vector3(VERT[boundary[i], 0], VERT[boundary[i], 1], 0.01f), transform.rotation) as GameObject;
                ancore[i].transform.SetParent(this.transform);
            }
            else
            {
                ancore[i] = Instantiate(ancora, new Vector3(VERT[boundary[i], 0], VERT[boundary[i], 1], 0.01f), transform.rotation) as GameObject;
                ancore[i].transform.SetParent(this.transform);
            }
            VERTINIZ[i] = ancore[i].transform.position;
        }
    }

    private float[,] ConvDvtoFv(double[,] A)
    {
        float[,] ris = new float[A.GetLength(0), A.GetLength(1)];
        for (int i = 0; i < A.GetLength(0); i++)
        {
            for (int j = 0; j < A.GetLength(1); j++)
            {
                ris[i, j] = (float)A[i, j];
            }
        }
        return ris;
    }

    private double[,] ConvFvtoDv(float[,] A)
    {
        double[,] ris = new double[A.GetLength(0), A.GetLength(1)];
        for (int i = 0; i < A.GetLength(0); i++)
        {
            for (int j = 0; j < A.GetLength(1); j++)
            {
                ris[i, j] = A[i, j];
            }
        }
        return ris;
    }

    private double[][] ConvFvtoDD(float[,] A)
    {
        double[][] ris = new double[A.GetLength(0)][];
        for (int i = 0; i < A.GetLength(0); i++)
        {
            ris[i] = new double[A.GetLength(1)];
            for (int j = 0; j < A.GetLength(1); j++)
            {
                ris[i][j] = A[i, j];
            }
        }
        return ris;
    }

    private float[,] ConvDDtoFv(double[][] A)
    {
        float[,] ris = new float[A.GetLength(0), A.GetLength(0)];
        for (int i = 0; i < A.GetLength(0); i++)
        {
            for (int j = 0; j < A.GetLength(0); j++)
            {
                ris[i, j] = (float)A[i][j];
            }
        }
        return ris;
    }

    private float[,] LaplaceBeltrami(float[,] M, float[,] S)
    {
        double[][] dainv = ConvFvtoDD(M);
        double[][] inv = MatrixInverse(dainv);
        float[,] invf = ConvDDtoFv(inv);
        float[,] ris = ProdottoRigheColonne(invf, S);
        return ris;
    }

    private float[,] Stiffness(float[,] V, int[,] T)
    {
        float[,] S = new float[V.GetLength(0), V.GetLength(0)];
        int[] X = new int[T.Length];
        int[] Y = new int[T.Length];
        int cont = 0;
        for (int j = 0; j < T.GetLength(1); j++)
        {
            for (int i = 0; i < T.GetLength(0); i++)
            {
                X[cont] = T[i, j];
                cont++;
            }
        }
        cont = 0;
        int col = 1;
        int contcol = 0;
        while (true)
        {
            for (int i = 0; i < T.GetLength(0); i++)
            {
                Y[cont] = T[i, col];
                cont++;
            }
            col = (col + 1) % 3;
            contcol++;
            if (contcol == 3) break;
        }
        float[] S012 = Sti(V, T, 0, 1, 2);
        float[] S120 = Sti(V, T, 1, 2, 0);
        float[] S201 = Sti(V, T, 2, 0, 1);
        float[] Sp = new float[T.Length];
        cont = 0;
        for (int j = 0; j < T.GetLength(1); j++)
        {
            for (int i = 0; i < T.GetLength(0); i++)
            {
                if (j == 0)
                {
                    Sp[cont] = S012[i];
                }
                else if (j == 1)
                {
                    Sp[cont] = S120[i];
                }
                else
                {
                    Sp[cont] = S201[i];
                }
                cont++;
            }
        }
        for (int i = 0; i < S.GetLength(0); i++)
        {
            for (int j = 0; j < S.GetLength(1); j++)
            {
                S[i, j] = 0;
            }
        }
        for (int i = 0; i < X.Length; i++)
        {
            S[X[i], Y[i]] = Sp[i];
        }
        S = Somma(S, Trasposta(S));
        S = PrPerSc(S, -(1f / 2f));
        for (int i = 0; i < V.GetLength(0); i++)
        {
            float somma = 0;
            for (int j = 0; j < V.GetLength(0); j++)
            {
                if (i == j) continue;
                somma += S[i, j];
            }
            S[i, i] = -somma;
        }
        return S;
    }

    private float[] Sti(float[,] V, int[,] T, int u, int v, int z)
    {
        float[] res = new float[T.GetLength(0)];
        for (int i = 0; i < T.GetLength(0); i++)
        {
            Vector3 v1 = new Vector3
                (V[T[i, u], 0] - V[T[i, z], 0],
                V[T[i, u], 1] - V[T[i, z], 1],
                V[T[i, u], 2] - V[T[i, z], 2]);
            Vector3 v2 = new Vector3
                (V[T[i, v], 0] - V[T[i, z], 0],
                V[T[i, v], 1] - V[T[i, z], 1],
                V[T[i, v], 2] - V[T[i, z], 2]);

            float cos_theta = Vector3.Dot(v1, v2) /
                ((float)Math.Sqrt(Math.Pow(v1.x, 2) + Math.Pow(v1.y, 2) + Math.Pow(v1.z, 2)) *
                (float)Math.Sqrt(Math.Pow(v2.x, 2) + Math.Pow(v2.y, 2) + Math.Pow(v2.z, 2)));
            res[i] = 1f / (float)Math.Tan(Math.Acos(cos_theta));
        }
        return res;
    }

    private float[,] Masse(float[,] V, int[,] T)
    {
        float[,] M = new float[V.GetLength(0), V.GetLength(0)];
        for (int i = 0; i < V.GetLength(0); i++)
        {
            float somma = 0;
            for (int j = 0; j < T.GetLength(0); j++)
            {
                for (int k = 0; k < T.GetLength(1); k++)
                {
                    if (T[j, k] == i)
                    {
                        Vector3 v1 = new Vector3(V[T[j, (k + 1) % 3], 0] - V[i, 0], V[T[j, (k + 1) % 3], 1] - V[i, 1], V[T[j, (k + 1) % 3], 2] - V[i, 2]);
                        Vector3 v2 = new Vector3(V[T[j, (k + 2) % 3], 0] - V[i, 0], V[T[j, (k + 2) % 3], 1] - V[i, 1], V[T[j, (k + 2) % 3], 2] - V[i, 2]);
                        Vector3 c = Vector3.Cross(v1, v2);
                        somma += (1f / 2f) * (float)Math.Sqrt(Math.Pow(c.x, 2) + Math.Pow(c.y, 2) + Math.Pow(c.z, 2));
                        break;
                    }
                }
            }
            M[i, i] = (1f / 3f) * somma;
        }
        return M;
    }

    private float[,] Sistema(float[,] A, float[,] b)
    {
        float[,] prg = ProdottoRigheColonne(Trasposta(A), A);
        double[][] dainv = ConvFvtoDD(prg);
        double[][] inv = MatrixInverse(dainv);
        float[,] invf = ConvDDtoFv(inv);
        float[,] ris = ProdottoRigheColonne(ProdottoRigheColonne(invf, Trasposta(A)), b);
        return ris;
    }

    private float[,] Trasposta(float[,] A)
    {
        float[,] res = new float[A.GetLength(1), A.GetLength(0)];
        for (int i = 0; i < A.GetLength(0); i++)
        {
            for (int j = 0; j < A.GetLength(1); j++)
            {
                res[j, i] = A[i, j];
            }
        }
        return res;
    }

    private float[,] Right(int[] b, float[,] vert)
    {
        float[,] res = new float[vert.GetLength(0) + b.Length, 3];
        for (int i = 0; i < res.GetLength(0); i++)
        {
            for (int j = 0; j < res.GetLength(1); j++)
            {
                if (i > vert.GetLength(0) - 1)
                {
                    res[i, j] = 100 * vert[b[i - vert.GetLength(0)], j];
                }
                else
                {
                    res[i, j] = 0;
                }
            }
        }
        return res;
    }

    private float[,] Left(float[,] L, int[] b)
    {
        float[,] res = new float[L.GetLength(0) + b.Length, L.GetLength(1)];
        for (int i = 0; i < res.GetLength(0); i++)
        {
            for (int j = 0; j < res.GetLength(1); j++)
            {
                if (i > L.GetLength(0) - 1)
                {
                    res[i, b[i - L.GetLength(0)]] = 100;
                }
                else
                {
                    res[i, j] = L[i, j];
                }
            }
        }
        return res;
    }

    private float[,] Denom(float[,] A, float num)
    {
        float[,] res = new float[A.GetLength(0), A.GetLength(1)];
        for (int i = 0; i < A.GetLength(0); i++)
        {
            for (int j = 0; j < A.GetLength(1); j++)
            {
                res[i, j] = num / A[i, j];
            }
        }
        return res;
    }

    private float[,] PrPerSc(float[,] A, float num)
    {
        float[,] res = new float[A.GetLength(0), A.GetLength(1)];
        for (int i = 0; i < A.GetLength(0); i++)
        {
            for (int j = 0; j < A.GetLength(1); j++)
            {
                res[i, j] = num * A[i, j];
            }
        }
        return res;
    }

    private float[,] Sottrai(float[,] A, float[,] B)
    {
        float[,] res = new float[A.GetLength(0), A.GetLength(1)];
        for (int i = 0; i < A.GetLength(0); i++)
        {
            for (int j = 0; j < A.GetLength(1); j++)
            {
                res[i, j] = A[i, j] - B[i, j];
            }
        }
        return res;
    }

    private float[,] Somma(float[,] A, float[,] B)
    {
        float[,] res = new float[A.GetLength(0), A.GetLength(1)];
        for (int i = 0; i < A.GetLength(0); i++)
        {
            for (int j = 0; j < A.GetLength(1); j++)
            {
                res[i, j] = A[i, j] + B[i, j];
            }
        }
        return res;
    }

    private float[,] Diag(float[,] num)
    {
        float[,] d = new float[num.Length, num.Length];
        for (int i = 0; i < num.Length; i++)
        {
            d[i, i] = num[i, 0];
        }
        return d;
    }

    //somma solo lungo la seconda dimensione
    private float[,] Sum(float[,] A)
    {
        float[,] res = new float[A.GetLength(0), 1];
        for (int i = 0; i < A.GetLength(0); i++)
        {
            res[i, 0] = 0;
            for (int j = 0; j < A.GetLength(1); j++)
            {
                res[i, 0] += A[i, j];
            }
        }
        return res;
    }

    private float[,] Eye(int num)
    {
        float[,] d = new float[num, num];
        for (int i = 0; i < num; i++)
        {
            d[i, i] = 1;
        }
        return d;
    }

    private float[,] ProdottoRigheColonne(float[,] A, float[,] B)
    {
        float[,] res = new float[A.GetLength(0), B.GetLength(1)];

        for (int i = 0; i < A.GetLength(0); i++)
        {
            for (int j = 0; j < B.GetLength(1); j++)
            {
                float RIS = 0f;
                for (int k = 0; k < A.GetLength(1); k++)
                {
                    RIS = RIS + (A[i, k] * B[k, j]);
                }
                res[i, j] = RIS;
            }
        }
        return res;
    }

    private float[,] CalcAdj(int[,] triv)
    {
        float[,] a = new float[n, n];

        for (int i = 0; i < triv.Length / 3; i++)
        {
            for (int j = 0; j < triv.Length / 3; j++)
            {
                a[triv[i, 0], triv[i, 1]] = 1f;
                a[triv[i, 1], triv[i, 2]] = 1f;
                a[triv[i, 2], triv[i, 0]] = 1f;
            }
        }
        return a;
    }

    static double[][] MatrixCreate(int rows, int cols)
    {
        double[][] result = new double[rows][];
        for (int i = 0; i < rows; ++i)
            result[i] = new double[cols];
        return result;
    }

    static double[][] MatrixInverse(double[][] matrix)
    {
        int n = matrix.Length;
        double[][] result = MatrixDuplicate(matrix);

        int[] perm;
        int toggle;
        double[][] lum = MatrixDecompose(matrix, out perm,
          out toggle);
        if (lum == null)
            throw new Exception("Unable to compute inverse");

        double[] b = new double[n];
        for (int i = 0; i < n; ++i)
        {
            for (int j = 0; j < n; ++j)
            {
                if (i == perm[j])
                    b[j] = 1.0;
                else
                    b[j] = 0.0;
            }

            double[] x = HelperSolve(lum, b);

            for (int j = 0; j < n; ++j)
                result[j][i] = x[j];
        }
        return result;
    }

    static double[][] MatrixDuplicate(double[][] matrix)
    {
        // allocates/creates a duplicate of a matrix.
        double[][] result = MatrixCreate(matrix.Length, matrix[0].Length);
        for (int i = 0; i < matrix.Length; ++i) // copy the values
            for (int j = 0; j < matrix[i].Length; ++j)
                result[i][j] = matrix[i][j];
        return result;
    }

    static double[] HelperSolve(double[][] luMatrix, double[] b)
    {
        // before calling this helper, permute b using the perm array
        // from MatrixDecompose that generated luMatrix
        int n = luMatrix.Length;
        double[] x = new double[n];
        b.CopyTo(x, 0);

        for (int i = 1; i < n; ++i)
        {
            double sum = x[i];
            for (int j = 0; j < i; ++j)
                sum -= luMatrix[i][j] * x[j];
            x[i] = sum;
        }

        x[n - 1] /= luMatrix[n - 1][n - 1];
        for (int i = n - 2; i >= 0; --i)
        {
            double sum = x[i];
            for (int j = i + 1; j < n; ++j)
                sum -= luMatrix[i][j] * x[j];
            x[i] = sum / luMatrix[i][i];
        }

        return x;
    }

    static double[][] MatrixDecompose(double[][] matrix, out int[] perm, out int toggle)
    {
        // Doolittle LUP decomposition with partial pivoting.
        // rerturns: result is L (with 1s on diagonal) and U;
        // perm holds row permutations; toggle is +1 or -1 (even or odd)
        int rows = matrix.Length;
        int cols = matrix[0].Length; // assume square
        if (rows != cols)
            throw new Exception("Attempt to decompose a non-square m");

        int n = rows; // convenience

        double[][] result = MatrixDuplicate(matrix);

        perm = new int[n]; // set up row permutation result
        for (int i = 0; i < n; ++i) { perm[i] = i; }

        toggle = 1; // toggle tracks row swaps.
                    // +1 -greater-than even, -1 -greater-than odd. used by MatrixDeterminant

        for (int j = 0; j < n - 1; ++j) // each column
        {
            double colMax = Math.Abs(result[j][j]); // find largest val in col
            int pRow = j;
            //for (int i = j + 1; i less-than n; ++i)
            //{
            //  if (result[i][j] greater-than colMax)
            //  {
            //    colMax = result[i][j];
            //    pRow = i;
            //  }
            //}

            // reader Matt V needed this:
            for (int i = j + 1; i < n; ++i)
            {
                if (Math.Abs(result[i][j]) > colMax)
                {
                    colMax = Math.Abs(result[i][j]);
                    pRow = i;
                }
            }
            // Not sure if this approach is needed always, or not.

            if (pRow != j) // if largest value not on pivot, swap rows
            {
                double[] rowPtr = result[pRow];
                result[pRow] = result[j];
                result[j] = rowPtr;

                int tmp = perm[pRow]; // and swap perm info
                perm[pRow] = perm[j];
                perm[j] = tmp;

                toggle = -toggle; // adjust the row-swap toggle
            }

            // --------------------------------------------------
            // This part added later (not in original)
            // and replaces the 'return null' below.
            // if there is a 0 on the diagonal, find a good row
            // from i = j+1 down that doesn't have
            // a 0 in column j, and swap that good row with row j
            // --------------------------------------------------

            if (result[j][j] == 0.0)
            {
                // find a good row to swap
                int goodRow = -1;
                for (int row = j + 1; row < n; ++row)
                {
                    if (result[row][j] != 0.0)
                        goodRow = row;
                }

                if (goodRow == -1)
                    throw new Exception("Cannot use Doolittle's method");

                // swap rows so 0.0 no longer on diagonal
                double[] rowPtr = result[goodRow];
                result[goodRow] = result[j];
                result[j] = rowPtr;

                int tmp = perm[goodRow]; // and swap perm info
                perm[goodRow] = perm[j];
                perm[j] = tmp;

                toggle = -toggle; // adjust the row-swap toggle
            }
            // --------------------------------------------------
            // if diagonal after swap is zero . .
            //if (Math.Abs(result[j][j]) less-than 1.0E-20) 
            //  return null; // consider a throw

            for (int i = j + 1; i < n; ++i)
            {
                result[i][j] /= result[j][j];
                for (int k = j + 1; k < n; ++k)
                {
                    result[i][k] -= result[i][j] * result[j][k];
                }
            }


        } // main j column loop

        return result;
    }

    public void Reset()
    {
        //VERT1 = VERTINIZ;
        //mesh.vertices = VERT1;
        //Vector2[] uvs = new Vector2[mesh.vertices.Length];
        //for (int i = 0; i < uvs.Length; i++)
        //{
        //    uvs[i] = new Vector2(mesh.vertices[i].x, mesh.vertices[i].y);
        //}
        //mesh.uv = uvs;
        //mesh.RecalculateBounds();
        for (int i = 0; i < ancore.Length; i++)
        {
            ancore[i].transform.position = VERTINIZ[i];
        }
    }

    private void Update()
    {
        if (menuVite.activeSelf || menuSconfitta.activeSelf || menu.activeSelf || vittoria.activeSelf || tutorial.activeSelf)
        {
            DragDrop.attivo = false;
            //for (int i = 0; i < boundary.Length; i++)
            //{
            //    ancore[i].SetActive(false);
            //}
            //attive = false;
        }
        else
        //else if((!menuVite.activeSelf || !menuSconfitta.activeSelf) && !attive)
        //{
        //    attive = true;
        //}
        //if((!menuVite.activeSelf || !menuSconfitta.activeSelf) && attive)
        {
            //for (int i = 0; i < boundary.Length; i++)
            //{
            //    ancore[i].SetActive(true);
            //}
            DragDrop.attivo = true;
            for (int i = 0; i < boundary.Length; i++)
            {
                VERT1[boundary[i]].x = ancore[i].transform.position.x;
                VERT1[boundary[i]].y = ancore[i].transform.position.y;
            }
            for (int i = 0; i < n; i++)
            {
                VERT[i, 0] = VERT1[i].x;
                VERT[i, 1] = VERT1[i].y;
                VERT[i, 2] = VERT1[i].z;
            }

            float[,] L = Sottrai(Eye(n), ProdottoRigheColonne(Diag(Denom(Sum(A), 1f)), A));
            float[,] left = Left(L, boundary);
            float[,] right = Right(boundary, VERT);
            float[,] VERT_is = Sistema(left, right);
            VERT = VERT_is;
            float[,] M = Masse(VERT, TRIV);
            float[,] S = Stiffness(VERT, TRIV);
            Accord.Math.Decompositions.GeneralizedEigenvalueDecomposition autov =
                new Accord.Math.Decompositions.GeneralizedEigenvalueDecomposition(ConvFvtoDv(S), ConvFvtoDv(M), true);
            float[,] autovalori = ConvDvtoFv(autov.DiagonalMatrix);
            eigenvalue = new float[autovalori.GetLength(0)];
            for (int i = 0; i < autovalori.GetLength(0); i++)
            {
                eigenvalue[i] = autovalori[i, i];
            }
            //eigenvalue1 = new float[eigenvalue.Length - 1];
            //for (int i = 1; i < eigenvalue.Length; i++)
            //{
            //    eigenvalue1[i - 1] = eigenvalue[i];
            //    print(eigenvalue[i]);
            //}
            float temp;
            int flag;
            int nn = eigenvalue.Length;
            int p = eigenvalue.Length;
            do
            {
                flag = 0;
                for (int i = 0; i < (nn - 1); i++)
                    if (eigenvalue[i] > eigenvalue[i + 1])
                    {
                        temp = eigenvalue[i];
                        eigenvalue[i] = eigenvalue[i + 1];
                        eigenvalue[i + 1] = temp;
                        flag = 1;
                        p = i + 1;
                    }
                nn = p;
            }
            while (flag == 1);
            for (int i = 0; i < VERT_is.GetLength(0); i++)
            {
                VERT1[i].x = VERT_is[i, 0];
                VERT1[i].y = VERT_is[i, 1];
                VERT1[i].z = VERT_is[i, 2];
            }

            mesh.vertices = VERT1;
            Vector2[] uvs = new Vector2[mesh.vertices.Length];
            for (int i = 0; i < uvs.Length; i++)
            {
                uvs[i] = new Vector2(mesh.vertices[i].x, mesh.vertices[i].y);
            }
            mesh.uv = uvs;
            mesh.RecalculateBounds();
        }
    }
}
