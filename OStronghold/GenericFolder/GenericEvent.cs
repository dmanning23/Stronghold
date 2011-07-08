using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OStronghold.GenericFolder
{
    public class GenericEventArgs:EventArgs
    {
        public string _message;
        public Consts.eventType _eventType;
        public int _eventlevel;

        public GenericEventArgs(string targetMessage, Consts.eventType targetEventType, int targetEventLevel)
        {            
            _message = string.Copy(targetMessage);
            _eventType = targetEventType;
            _eventlevel = targetEventLevel;            
        }
    }

    public class GenericEvent
    {
        public delegate void GenericEventHandler(object sender, GenericEventArgs e);
        public event GenericEventHandler writeEventToFile;

        public void writeEvent(string message, Consts.eventType eventType, int eventLevel)
        {
            Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            GenericEventArgs eventArgs = new GenericEventArgs(message, eventType, eventLevel);

            writeEventToFile(this, eventArgs);
            Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
    }

    public class GenericEventHandlerClass
    {
        public GenericEventHandlerClass(GenericEvent targetEvent)
        {            
            targetEvent.writeEventToFile += new GenericEvent.GenericEventHandler(eventWritingToFile);            
        }

        public void eventWritingToFile(object sender, GenericEventArgs e)
        {
            Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            if (e._eventlevel <= Consts.CURRENT_EVENT_DEBUG_LEVEL)
            {
                switch (e._eventType)
                {
                    case Consts.eventType.Character:
                        Consts.writeToCharacterEventLog(e._message);
                        break;
                    case Consts.eventType.Building:
                        Consts.writeToBuildingEventLog(e._message);
                        break;
                    case Consts.eventType.Stronghold:
                        Consts.writeToStrongholdEventLog(e._message);
                        break;
                }
            }
            Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

    }
}
