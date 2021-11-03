using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour {

    public Texture[] texture;
    public Renderer renderer;
    public Material mat;

	void Awake () {
        renderer = GetComponent<Renderer>();
        mat = new Material(Shader.Find("Particles/Additive"));
        renderer.material = mat;
        mat.mainTexture = texture[0];

    }

    void OnEnable()
    {
        mat.mainTexture = texture[0];
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        for(int i = 0; i < texture.Length; i++)
        {
            mat.mainTexture = texture[i];
            yield return new WaitForSeconds(0.1f); 
        }

        gameObject.SetActive(false);
    }

}
