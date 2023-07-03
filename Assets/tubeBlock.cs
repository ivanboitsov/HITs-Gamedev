using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tubeBlock : MonoBehaviour
{
    public string type;
    public float rotationState = 0f;
    public int idMatrix = 0;
    public bool activated = false;
    public tubesPuzzleController mainController = null;
    private Renderer _renderer;

    private void OnMouseDown()
    {
        rotationState += 90f;
        if (rotationState >= 360f)
        {
            rotationState = 0f;
        }
        transform.rotation = Quaternion.Euler(180f, 0f, rotationState+180f);
        GetNext();
    }

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (activated)
        {
            foreach (Material mat in _renderer.materials)
            {
                mat.SetColor("_EmissionColor", Color.white);
            }
        }
        else
        {
            foreach (Material mat in _renderer.materials)
            {
                mat.SetColor("_EmissionColor", Color.gray);
            }
        }
    }

    public bool myNexts(int id)
    {
        if (type == "straight")
        {
            if (rotationState % 180f != 0)
            {
                if (id == idMatrix - 1 || id == idMatrix + 1)
                {
                    return true && activated;
                }
            }
            else
            {
                if (id == idMatrix - 4 || id == idMatrix + 4)
                {
                    return true && activated;
                }
            }
        }
        else if (type == "cross")
        {
            return activated;
        }
        else if (type == "side")
        {
            if (rotationState == 0f)
            {
                if (id == idMatrix - 4 || id == idMatrix + 1)
                {
                    return true && activated;
                }
            }
            else if (rotationState == 90f)
            {
                if (id == idMatrix + 4 || id == idMatrix + 1)
                {
                    return true && activated;
                }
            }
            else if (rotationState == 180f)
            {
                if (id == idMatrix + 4 || id == idMatrix - 1)
                {
                    return true && activated;
                }
            }
            else if (rotationState == 270f)
            {
                if (id == idMatrix - 4 || id == idMatrix - 1)
                {
                    return true && activated;
                }
            }
        }
        else if (type == "double")
        {
            if (rotationState == 0f)
            {
                if (id == idMatrix - 4 || id == idMatrix + 1 || id == idMatrix - 4)
                {
                    return true && activated;
                }
            }
            else if (rotationState == 90f)
            {
                if (id == idMatrix - 1 || id == idMatrix + 1 || id == idMatrix + 4)
                {
                    return true && activated;
                }
            }
            else if (rotationState == 180f)
            {
                if (id == idMatrix - 4 || id == idMatrix - 1 || id == idMatrix - 4)
                {
                    return true && activated;
                }
            }
            else if (rotationState == 270f)
            {
                if (id == idMatrix - 1 || id == idMatrix + 1 || id == idMatrix - 4)
                {
                    return true && activated;
                }
            }
        }
        return false;
    }

    public void CheckNeighbours()
    {
        bool aPrev = false;
        if (type == "straight")
        {
            if (rotationState % 180f != 0)
            {
                aPrev = aPrev || mainController.CheckState(idMatrix - 1, idMatrix);
                aPrev = aPrev || mainController.CheckState(idMatrix + 1, idMatrix);
            }
            else
            {
                aPrev = aPrev || mainController.CheckState(idMatrix - 4, idMatrix);
                aPrev = aPrev || mainController.CheckState(idMatrix + 4, idMatrix);
            }
        }
        else if (type == "cross")
        {
            aPrev = aPrev || mainController.CheckState(idMatrix - 1, idMatrix);
            aPrev = aPrev || mainController.CheckState(idMatrix + 1, idMatrix);
            aPrev = aPrev || mainController.CheckState(idMatrix - 4, idMatrix);
            aPrev = aPrev || mainController.CheckState(idMatrix + 4, idMatrix);
        }
        else if (type == "side")
        {
            if (rotationState == 0f)
            {
                aPrev = aPrev || mainController.CheckState(idMatrix - 4, idMatrix);
                aPrev = aPrev || mainController.CheckState(idMatrix + 1, idMatrix);
            }
            else if (rotationState == 90f)
            {
                aPrev = aPrev || mainController.CheckState(idMatrix + 4, idMatrix);
                aPrev = aPrev || mainController.CheckState(idMatrix + 1, idMatrix);
            }
            else if (rotationState == 180f)
            {
                aPrev = aPrev || mainController.CheckState(idMatrix + 4, idMatrix);
                aPrev = aPrev || mainController.CheckState(idMatrix - 1, idMatrix);
            }
            else if (rotationState == 270f)
            {
                aPrev = aPrev || mainController.CheckState(idMatrix - 4, idMatrix);
                aPrev = aPrev || mainController.CheckState(idMatrix - 1, idMatrix);
            }
        }
        else if (type == "double")
        {
            if (rotationState == 0f)
            {
                aPrev = aPrev || mainController.CheckState(idMatrix - 4, idMatrix);
                aPrev = aPrev || mainController.CheckState(idMatrix + 1, idMatrix);
                aPrev = aPrev || mainController.CheckState(idMatrix + 4, idMatrix);
            }
            else if (rotationState == 90f)
            {
                aPrev = aPrev || mainController.CheckState(idMatrix + 4, idMatrix);
                aPrev = aPrev || mainController.CheckState(idMatrix + 1, idMatrix);
                aPrev = aPrev || mainController.CheckState(idMatrix - 1, idMatrix);
            }
            else if (rotationState == 180f)
            {
                aPrev = aPrev || mainController.CheckState(idMatrix + 4, idMatrix);
                aPrev = aPrev || mainController.CheckState(idMatrix - 1, idMatrix);
                aPrev = aPrev || mainController.CheckState(idMatrix - 4, idMatrix);
            }
            else if (rotationState == 270f)
            {
                aPrev = aPrev || mainController.CheckState(idMatrix - 4, idMatrix);
                aPrev = aPrev || mainController.CheckState(idMatrix - 1, idMatrix);
                aPrev = aPrev || mainController.CheckState(idMatrix + 1, idMatrix);
            }
        }
        activated = aPrev;
    }

    public void GetNext()
    {
        if (activated)
        {
            if (type == "straight")
            {
                if (rotationState % 180f != 0)
                {
                    if ((idMatrix - 1)/4 * 4 == (idMatrix) / 4 * 4)
                    {
                        mainController.ActivateSingle(idMatrix - 1, idMatrix);
                    }
                    if ((idMatrix + 1) / 4 * 4 == (idMatrix) / 4 * 4)
                    {
                        mainController.ActivateSingle(idMatrix + 1, idMatrix);
                    }
                }
                else
                {
                    mainController.ActivateSingle(idMatrix - 4, idMatrix);
                    mainController.ActivateSingle(idMatrix + 4, idMatrix);
                }
            }
            else if (type == "cross")
            {
                if ((idMatrix - 1) / 4 * 4 == (idMatrix) / 4 * 4)
                {
                    mainController.ActivateSingle(idMatrix - 1, idMatrix);
                }
                if ((idMatrix + 1) / 4 * 4 == (idMatrix) / 4 * 4)
                {
                    mainController.ActivateSingle(idMatrix + 1, idMatrix);
                }
                mainController.ActivateSingle(idMatrix - 4, idMatrix);
                mainController.ActivateSingle(idMatrix + 4, idMatrix);
            }
            else if (type == "side")
            {
                if (rotationState == 0f)
                {
                    mainController.ActivateSingle(idMatrix - 4, idMatrix);
                    if ((idMatrix + 1) / 4 * 4 == (idMatrix) / 4 * 4)
                    {
                        mainController.ActivateSingle(idMatrix + 1, idMatrix);
                    }
                }
                else if (rotationState == 90f)
                {
                    mainController.ActivateSingle(idMatrix + 4, idMatrix);
                    if ((idMatrix + 1) / 4 * 4 == (idMatrix) / 4 * 4)
                    {
                        mainController.ActivateSingle(idMatrix + 1, idMatrix);
                    }
                }
                else if (rotationState == 180f)
                {
                    mainController.ActivateSingle(idMatrix + 4, idMatrix);
                    if ((idMatrix - 1) / 4 * 4 == (idMatrix) / 4 * 4)
                    {
                        mainController.ActivateSingle(idMatrix - 1, idMatrix);
                    }
                }
                else if (rotationState == 270f)
                {
                    mainController.ActivateSingle(idMatrix - 4, idMatrix);
                    if ((idMatrix - 1) / 4 * 4 == (idMatrix) / 4 * 4)
                    {
                        mainController.ActivateSingle(idMatrix - 1, idMatrix);
                    }
                }
            }
            else if (type == "double")
            {
                if (rotationState == 0f)
                {
                    mainController.ActivateSingle(idMatrix - 4, idMatrix);
                    if ((idMatrix + 1) / 4 * 4 == (idMatrix) / 4 * 4)
                    {
                        mainController.ActivateSingle(idMatrix + 1, idMatrix);
                    }
                    mainController.ActivateSingle(idMatrix + 4, idMatrix);
                }
                else if (rotationState == 90f)
                {
                    mainController.ActivateSingle(idMatrix + 4, idMatrix);
                    if ((idMatrix + 1) / 4 * 4 == (idMatrix) / 4 * 4)
                    {
                        mainController.ActivateSingle(idMatrix + 1, idMatrix);
                    }
                    if ((idMatrix - 1) / 4 * 4 == (idMatrix) / 4 * 4)
                    {
                        mainController.ActivateSingle(idMatrix - 1, idMatrix);
                    }
                }
                else if (rotationState == 180f)
                {
                    mainController.ActivateSingle(idMatrix + 4, idMatrix);
                    if ((idMatrix - 1) / 4 * 4 == (idMatrix) / 4 * 4)
                    {
                        mainController.ActivateSingle(idMatrix - 1, idMatrix);
                    }
                    mainController.ActivateSingle(idMatrix - 4, idMatrix);
                }
                else if (rotationState == 270f)
                {
                    mainController.ActivateSingle(idMatrix - 4, idMatrix);
                    if ((idMatrix - 1) / 4 * 4 == (idMatrix) / 4 * 4)
                    {
                        mainController.ActivateSingle(idMatrix - 1, idMatrix);
                    }
                    if ((idMatrix + 1) / 4 * 4 == (idMatrix) / 4 * 4)
                    {
                        mainController.ActivateSingle(idMatrix + 1, idMatrix);
                    }
                }
            }
        }
    }

    public void Activate(int prev)
    {
        if (type == "straight")
        {
            UnityEngine.Debug.Log(prev.ToString() + " " + idMatrix.ToString() + " " + rotationState.ToString());
            if (rotationState == 0f || rotationState == 180f)
            {
                if (prev == idMatrix - 4 || prev == idMatrix + 4)
                {
                    activated = true;
                }
            }
            else if (rotationState == 90f || rotationState == 270f)
            {
                if (prev == idMatrix - 1 || prev == idMatrix + 1)
                {
                    activated = true;
                }
            }
            else
            {
                activated = false;
            }
        }
        else if (type == "side")
        {
            if (rotationState == 0f)
            {
                if (prev == idMatrix - 4 || prev == idMatrix + 1)
                {
                    activated = true;
                }
            }
            else if (rotationState == 90f)
            {
                if (prev == idMatrix + 1 || prev == idMatrix + 4)
                {
                    activated = true;
                }
            }
            else if (rotationState == 180f)
            {
                if (prev == idMatrix + 4 || prev == idMatrix - 1)
                {
                    activated = true;
                }
            }
            else if (rotationState == 270f)
            {
                if (prev == idMatrix - 1 || prev == idMatrix - 4)
                {
                    activated = true;
                }
            }
            else
            {
                activated = false;
            }
        }
        else if (type == "cross")
        {
            activated = true;
        }
        else if (type == "double")
        {
            if (rotationState == 0f)
            {
                if (prev == idMatrix - 4 || prev == idMatrix + 1 || prev == idMatrix + 4)
                {
                    activated = true;
                }
            }
            else if (rotationState == 90f)
            {
                if (prev == idMatrix + 1 || prev == idMatrix + 4 || prev == idMatrix - 1)
                {
                    activated = true;
                }
            }
            else if (rotationState == 180f)
            {
                if (prev == idMatrix + 4 || prev == idMatrix - 1 || prev == idMatrix - 4)
                {
                    activated = true;
                }
            }
            else if (rotationState == 270f)
            {
                if (prev == idMatrix - 1 || prev == idMatrix - 4 || prev == idMatrix + 1)
                {
                    activated = true;
                }
            }
            else
            {
                activated = false;
            }
        }

        if (activated)
        {
            GetNext();
        }
    }

    public int GetId()
    {
        return idMatrix;
    }
}
