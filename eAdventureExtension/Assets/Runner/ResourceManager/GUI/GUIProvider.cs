﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIProvider{

    public const string DEFAULT_ASSET_DIRECTORY = "gui/hud/contextual/";
    
    private static class ActionNameWrapper{
        static Dictionary<int,string> names = null;
        static Dictionary<int,string> auxNames = null;
        static Dictionary<string,int> ids = null;
        static Dictionary<string,int> auxids = null;

        public static Dictionary<int,string> Names {
            get {
                if (names == null) {
                    names = new Dictionary<int, string> ();

                    names.Add(Action.EXAMINE, DescriptorData.EXAMINE_BUTTON);
                    names.Add(Action.GRAB, DescriptorData.GRAB_BUTTON);
                    names.Add(Action.GIVE_TO, DescriptorData.GIVETO_BUTTON);
                    names.Add(Action.USE_WITH, DescriptorData.USEWITH_BUTTON);
                    names.Add(Action.USE, DescriptorData.USE_BUTTON);
                    names.Add(Action.CUSTOM, "");
                    names.Add(Action.CUSTOM_INTERACT, "");
                    names.Add(Action.TALK_TO, DescriptorData.TALK_BUTTON);
                    names.Add(Action.DRAG_TO, DescriptorData.DRAGTO_BUTTON);
                }
                return names;
            }
        }

        public static Dictionary<int,string> AuxNames {
            get {
                if (auxNames == null) {
                    auxNames = new Dictionary<int, string> ();
                    auxNames.Add(Action.USE, DescriptorData.USE_GRAB_BUTTON);
                    auxNames.Add(Action.GRAB, DescriptorData.USE_GRAB_BUTTON);
                }
                return auxNames;
            }
        }

        public static Dictionary<string,int> IDs {
            get {
                if (ids == null) {
                    ids = new Dictionary<string, int> ();
                    foreach(KeyValuePair<int,string> name in ActionNameWrapper.Names){
                        if (name.Value != "")
                            ids.Add (name.Value, name.Key);
                    }

                    foreach(KeyValuePair<int,string> name in ActionNameWrapper.AuxNames){
                        if (name.Value != "" && !ids.ContainsKey(name.Value))
                            ids.Add (name.Value, name.Key);
                    }
                }
                return ids;
            }
        }
    }   

    private static class DefaultActionAssetWrapper{
        static Dictionary<int,List<string>> assets = null;

        public static Dictionary<int,List<string>> Assets {
            get {
                if (assets == null) {
                    assets = new Dictionary<int, List<string>> ();

                    List<string> examine = new List<string> ();
                    examine.Add ("btnExamine");
                    examine.Add ("btnEye");
                    assets.Add (Action.EXAMINE, examine);

                    List<string> dragto = new List<string> ();
                    dragto.Add ("btnDragTo");
                    dragto.Add ("btnHand");
                    assets.Add (Action.DRAG_TO, dragto);

                    List<string> giveto = new List<string> ();
                    giveto.Add ("btnGiveTo");
                    assets.Add (Action.GIVE_TO, giveto);

                    List<string> grab = new List<string> ();
                    grab.Add ("btnGrab");
                    grab.Add ("btnHand");
                    assets.Add (Action.GRAB, grab);

                    List<string> use = new List<string> ();
                    use.Add ("btnUse");
                    use.Add ("btnHand");
                    assets.Add (Action.USE, use);

                    List<string> talk = new List<string> ();
                    talk.Add ("btnTalkTo");
                    talk.Add ("btnMouth");
                    assets.Add (Action.TALK_TO, talk);

                    List<string> usewith = new List<string> ();
                    usewith.Add ("btnUseWith");
                    usewith.Add ("btnHand");
                    assets.Add (Action.USE_WITH, usewith);
                }
                return assets;
            }
        }
    }  

    int guitype;

    Dictionary<int,ResourcesUni> buttons;

    public GUIProvider(AdventureData data){
        this.guitype = data.getGUIType ();


        ResourcesUni auxResource;
        buttons = new Dictionary<int, ResourcesUni> ();

        //We add a resource for each button parsed in descriptor file
        foreach (CustomButton cb in data.getButtons()) {
            if (buttons.ContainsKey (ActionNameWrapper.IDs [cb.getAction ()])) {
                buttons [ActionNameWrapper.IDs [cb.getAction ()]].addAsset (new Asset (cb.getType (), cb.getPath ()));
            }else{
                auxResource = new ResourcesUni ();
                auxResource.addAsset (new Asset (cb.getType (), cb.getPath ()));
                buttons.Add (ActionNameWrapper.IDs [cb.getAction ()], auxResource);
            }
        }
            
        //For each button that isn on descriptor file we try to find the default assets;
        Texture2D auxTexture;
        foreach (KeyValuePair<int,string> button in ActionNameWrapper.Names) {
            if (!buttons.ContainsKey (button.Key)) {
                if (DefaultActionAssetWrapper.Assets.ContainsKey (button.Key)) {
                    string selected = "";
                    foreach (string name in DefaultActionAssetWrapper.Assets[button.Key]) {
                        auxTexture = ResourceManager.Instance.getImage (GUIProvider.DEFAULT_ASSET_DIRECTORY + name + ".png");

                        if (auxTexture != null) {
                            selected = name;
                            break;
                        }
                    }

                    if (selected != "") {
                        auxResource = new ResourcesUni ();
                        auxResource.addAsset (DescriptorData.NORMAL_BUTTON, GUIProvider.DEFAULT_ASSET_DIRECTORY + selected + ".png");
                        auxResource.addAsset (DescriptorData.HIGHLIGHTED_BUTTON, GUIProvider.DEFAULT_ASSET_DIRECTORY + selected + "Highlighted.png");
                        auxResource.addAsset (DescriptorData.PRESSED_BUTTON, GUIProvider.DEFAULT_ASSET_DIRECTORY + selected + "Pressed.png");
                        buttons.Add (button.Key, auxResource);
                    }
                }
            }
        }

    }

    public ResourcesUni getButton (Action action){
        if(buttons.ContainsKey(action.getType ()))
            return buttons [action.getType ()];
        else
            return null;
    }
    public ResourcesUni getButton (int action){
        if(buttons.ContainsKey(action))
            return buttons [action];
        else
            return null;
    }
    public ResourcesUni getButton (string action){
        if(ActionNameWrapper.IDs.ContainsKey(action) && buttons.ContainsKey(ActionNameWrapper.IDs[action]))
            return buttons [ActionNameWrapper.IDs[action]];
        else
            return null;
    }

    /*ResourcesUni getCursor (Action action);
    ResourcesUni getCursor (int action);
    ResourcesUni getCursor (string name);*/
}

