import { HubConnectionState, HubConnectionBuilder, LogLevel } from "@microsoft/signalr"
import { serverUrl } from "./api-client";

const connection = new HubConnectionBuilder()
    .withUrl(`${serverUrl}/shoppingListHub`)
    .configureLogging(LogLevel.Information)
    .build();



export async function startSignalR() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(startSignalR, 5000);
    }
}

connection.onclose(async () => {
    await startSignalR();
});


export async function sendTestMessageAsync() {
    console.log("Send test")
    await connection.invoke("Listen", "123")
}

export async function startListeningToShoppingListChanges(listId: string): Promise<boolean> {
    // If the connection isn't yet established, then wait some seconds. 
    if (connection.state != HubConnectionState.Connected)
        await new Promise(r => setTimeout(r, 2000));

    console.log(`Start listening to shopping list ${listId}`)
    let isConnected = await connection.invoke("StartListeningToShoppingListChanges", listId)
    return isConnected;
}
export async function stopListeningToShoppingListChanges(listId: string): Promise<boolean> {
    console.log(`Stop listening to shopping list ${listId}`)
    let isDisconnceted = await connection.invoke("StopListeningToShoppingListChanges", listId)
    return isDisconnceted;
}
