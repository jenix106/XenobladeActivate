using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;

namespace XenobladeActivate
{
    public class XenobladeModule : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            base.OnItemLoaded(item);
            item.gameObject.AddComponent<XenobladeComponent>();
        }
    }
    public class XenobladeComponent : MonoBehaviour
    {
        Item item;
        private Animation _animation;
        private bool active = false;
        private bool animating = false;
        public void Start()
        {
            item = GetComponent<Item>();
            _animation = item.GetComponentInChildren<Animation>();
            item.OnHeldActionEvent += Item_OnHeldActionEvent;
            item.OnGrabEvent += Item_OnGrabEvent;
            item.OnSnapEvent += Item_OnSnapEvent;
            Deactivate();
        }

        private void Item_OnSnapEvent(Holder holder)
        {
            Deactivate();
        }

        private void Item_OnGrabEvent(Handle handle, RagdollHand ragdollHand)
        {
            if(ragdollHand.creature != Player.local.creature)
            {
                ToggleActivate(); 
                if (active)
                {
                    if (item.gameObject.transform.Find("ActiveHandle"))
                    {
                        handle.Release();
                        ragdollHand.Grab(item.gameObject.transform.Find("ActiveHandle").GetComponent<Handle>());
                    }
                }
                else if (!active)
                {
                    if (item.gameObject.transform.Find("DeactiveHandle"))
                    {
                        handle.Release();
                        ragdollHand.Grab(item.gameObject.transform.Find("DeactiveHandle").GetComponent<Handle>());
                    }
                }
            }
        }

        private void Item_OnHeldActionEvent(RagdollHand ragdollHand, Handle handle, Interactable.Action action)
        {
            if (!animating && ((action == Interactable.Action.UseStart && ragdollHand.playerHand.controlHand.alternateUsePressed) || (action == Interactable.Action.AlternateUseStart && ragdollHand.playerHand.controlHand.usePressed)))
            {
                ToggleActivate();
                if (active)
                {
                    if (item.gameObject.transform.Find("ActiveHandle"))
                    {
                        handle.Release();
                        ragdollHand.Grab(item.gameObject.transform.Find("ActiveHandle").GetComponent<Handle>());
                    }
                }
                else if (!active)
                {
                    if (item.gameObject.transform.Find("DeactiveHandle"))
                    {
                        handle.Release();
                        ragdollHand.Grab(item.gameObject.transform.Find("DeactiveHandle").GetComponent<Handle>());
                    }
                }
            }
        }
        public void ToggleActivate()
        {
            animating = true;
            active = !active;
            if (active) _animation.Play("Activate");
            else if (!active) _animation.Play("Deactivate");
            item.gameObject.transform.Find("ActiveBlunt")?.gameObject.SetActive(active);
            item.gameObject.transform.Find("DeactiveBlunt")?.gameObject.SetActive(!active);
            item.gameObject.transform.Find("ActiveBlade")?.gameObject.SetActive(active);
            item.gameObject.transform.Find("DeactiveBlade")?.gameObject.SetActive(!active);
            item.gameObject.transform.Find("ActiveHandle")?.gameObject.SetActive(active);
            item.gameObject.transform.Find("DeactiveHandle")?.gameObject.SetActive(!active);
            item.gameObject.transform.Find("Pierce")?.gameObject.GetComponent<Damager>().UnPenetrateAll();
            item.gameObject.transform.Find("Slash")?.gameObject.GetComponent<Damager>().UnPenetrateAll();
            animating = false;
        }
        public void Activate()
        {
            animating = true;
            active = true;
            _animation.Play("Activate");
            item.gameObject.transform.Find("ActiveBlunt")?.gameObject.SetActive(active);
            item.gameObject.transform.Find("DeactiveBlunt")?.gameObject.SetActive(!active);
            item.gameObject.transform.Find("ActiveBlade")?.gameObject.SetActive(active);
            item.gameObject.transform.Find("DeactiveBlade")?.gameObject.SetActive(!active);
            item.gameObject.transform.Find("ActiveHandle")?.gameObject.SetActive(active);
            item.gameObject.transform.Find("DeactiveHandle")?.gameObject.SetActive(!active);
            item.gameObject.transform.Find("Pierce")?.gameObject.GetComponent<Damager>().UnPenetrateAll();
            item.gameObject.transform.Find("Slash")?.gameObject.GetComponent<Damager>().UnPenetrateAll();
            animating = false;
        }
        public void Deactivate()
        {
            animating = true;
            active = false;
            _animation.Play("Deactivate");
            item.gameObject.transform.Find("ActiveBlunt")?.gameObject.SetActive(active);
            item.gameObject.transform.Find("DeactiveBlunt")?.gameObject.SetActive(!active);
            item.gameObject.transform.Find("ActiveBlade")?.gameObject.SetActive(active);
            item.gameObject.transform.Find("DeactiveBlade")?.gameObject.SetActive(!active);
            item.gameObject.transform.Find("ActiveHandle")?.gameObject.SetActive(active);
            item.gameObject.transform.Find("DeactiveHandle")?.gameObject.SetActive(!active);
            item.gameObject.transform.Find("Pierce")?.gameObject.GetComponent<Damager>().UnPenetrateAll();
            item.gameObject.transform.Find("Slash")?.gameObject.GetComponent<Damager>().UnPenetrateAll();
            animating = false;
        }
    }
}
