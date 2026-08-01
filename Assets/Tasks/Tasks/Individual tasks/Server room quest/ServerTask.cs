using UnityEngine;

public class ServerTask : Task
{
    [Header("References")]
    public KeyCardDesk desk;
    public KeyCardReader reader;
    public ServerResetButton resetButton;


    private bool serverReset;

    public bool CanReturnKeycard => serverReset;


    public override void Activate()
    {
        base.Activate();
        //Debug.Log("ServerTask activated");


        requiredAmount = 1;
        serverReset = false;


        desk.task = this;

        desk.SpawnKeycard();
        desk.SetTaskHighlight(true);


        desk.OnKeycardTaken += KeycardTaken;
        desk.OnKeycardReturned += KeycardReturned;

        resetButton.OnResetPressed += ServerReset;
        //Debug.Log("Subscribed to reset button");


        reader.LockDoor();
    }



    public override void ResetTask()
    {
        base.ResetTask();
        //Debug.Log("ServerTask reset");

        desk.OnKeycardTaken -= KeycardTaken;
        desk.OnKeycardReturned -= KeycardReturned;

        resetButton.OnResetPressed -= ServerReset;


        serverReset = false;


        reader.LockDoor();
        desk.ClearKeycard();
    }



    private void KeycardTaken()
    {
        Debug.Log("Keycard taken");
        desk.SetTaskHighlight(false);

        reader.SetTaskHighlight(true);
    }



    private void ServerReset()
    {
        Debug.Log("Beebbooppb");
        serverReset = true;


        resetButton.SetTaskHighlight(false);

        desk.SetTaskHighlight(true);
    }



    private void KeycardReturned()
    {
        Debug.Log("Keycard has been returned");
        reader.LockDoor();

        desk.SetTaskHighlight(false);

        AddProgress();
    }
}