﻿using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class Texture2DHolder {

	public static Texture2D LoadTexture(string filePath) {
		
		Texture2D tex = null;
		byte[] fileData;
		
		if (System.IO.File.Exists(filePath))     {
			fileData = System.IO.File.ReadAllBytes(filePath);
			tex = new Texture2D(2, 2);
			tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
		}
		return tex;
	}
	public static byte[] LoadBytes(string filePath) {
		byte[] fileData = null;
		
		if (System.IO.File.Exists(filePath))
			fileData = System.IO.File.ReadAllBytes(filePath);
		
		return fileData;
	}



	byte[] fileData;
	private Texture2D tex;
	public Texture2D Texture {
		get { 
			if (this.tex == null) {
				tex = new Texture2D(2, 2,TextureFormat.BGRA32,false);
				tex.LoadImage(fileData);
				this.fileData = null;
			}
			
			return tex;
		}
		set { tex = value; }
	}

	public Texture2DHolder(byte[] data){
		this.fileData = data;
	}

    bool loaded = false;
    public bool Loaded(){
        return loaded;
    }

	public Texture2DHolder(string path){
        loaded = true;
        if(!path.Contains(Game.Instance.getSelectedGame()))
            path = Game.Instance.getSelectedGame() + path;

		this.fileData = LoadBytes(path);

		if(this.fileData==null){
			Regex pattern = new Regex("[óñ]");
			path = pattern.Replace(path, "+¦");
			
			this.fileData = LoadBytes(path);
			
            if (this.fileData == null) {
                loaded = false;
                Debug.Log ("No se pudo cargar: " + path);
            }
		}
	}
}
