import { HubConnectionState, HubConnectionBuilder, LogLevel } from "@microsoft/signalr"
import { shoppingListStore } from "../../store";
import { removeEntryWithId } from "../../utilities/array";
import { serverUrl } from "../api/api-client";
import type { EntryCreatedDto } from "./EntryCreatedDto";

let connectedShoppingList: string;

const connection = new HubConnectionBuilder()
    .withUrl(`${serverUrl}/shoppingListHub`)
    .configureLogging(LogLevel.Information)
    .build();



export async function startSignalR(): Promise<void> {
    try {
        if (connection.state == HubConnectionState.Connected) {
            console.log("Already connected to the hub.")
            return;
        }

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
    console.log(`Remove entry ${entryId} from shopping list ${shoppingListId}.`);
    shoppingListStore.update(shoppingList => {

        shoppingList.entries = removeEntryWithId(shoppingList.entries, entryId);

        return shoppingList;
    })

});

connection.on("CreateEntryOnShoppingList", (shoppingListId: string, entry: EntryCreatedDto) => {
    console.log(`Add entry ${entry.itemId} to shopping list ${shoppingListId} ${JSON.stringify(entry)}).`)

    shoppingListStore.update(shoppingList => {
        shoppingList.entries.push(entry)
        return shoppingList;
    })
});


export async function sendTestMessageAsync() {
    console.log("Send test")
    await connection.invoke("Listen", "123")
}

export async function startListeningToShoppingListChanges(listId: string): Promise<boolean> {
    if (!!connectedShoppingList) {
        console.log(`Already listening to ${listId}`);
        return;
    }

    // If the connection isn't yet established, then wait some seconds. 
    if (connection.state != HubConnectionState.Connected)
        await new Promise(r => setTimeout(r, 2000));

    console.log(`Start listening to shopping list ${listId}`)
    let isConnected = await connection.invoke("StartListeningToShoppingListChanges", listId)

    connectedShoppingList = listId;

    return isConnected;
}
export async function stopListeningToShoppingListChanges(listId: string): Promise<boolean> {
    console.log(`Stop listening to shopping list ${listId}`)
    let isDisconnceted = await connection.invoke("StopListeningToShoppingListChanges", listId)

    connectedShoppingList = undefined;

    return isDisconnceted;
}
