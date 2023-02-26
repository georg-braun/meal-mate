import { HubConnectionState, HubConnectionBuilder, LogLevel } from "@microsoft/signalr"
import { serverUrl } from "./api-client";

const connection = new HubConnectionBuilder()
    .withUrl(`${serverUrl}/shoppingListHub`)
    .configureLogging(LogLevel.Information)
    .build();



export async function startSignalR(): Promise<void> {
    try {
        await connection.start();
        console.log("SignalR connected.");
    } catch (err) {
        console.log(err);
        setTimeout(startSignalR, 5000);
    }
}

export async function stopSignalR(): Promise<void> {
    try {
        await connection.stop();
        console.log("SignalR disconneted.");
    } catch (err) {
        console.log(err);
        setTimeout(startSignalR, 5000);
    }
}

connection.onclose(async () => {
    await startSignalR();
});


connection.on("RemoveEntryFromShoppingList", (shoppingListId: string, entryId: string) => {
    console.log(`Remove entry ${entryId} from shopping list ${shoppingListId}.`)

});

connection.on("CreateEntryOnShoppingList", (shoppingListId: string, entryId: string) => {
    console.log(`Add entry ${entryId} to shopping list ${shoppingListId}.`)

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
