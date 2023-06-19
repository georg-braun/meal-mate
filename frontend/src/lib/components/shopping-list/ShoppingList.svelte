<script lang="ts">
  import { onDestroy } from "svelte";

  import {
    startListeningToShoppingListChanges,
    startSignalR,
    stopListeningToShoppingListChanges,
    stopSignalR,
  } from "../../communication/hub/mealMateHub";
  import { shoppingListStore } from "../../store";
  import ShoppingListEntry from "./ShoppingListEntry.svelte";
  import apiClient from "../../communication/api/api-client";
  import ActionButton from "../ActionButton.svelte";

  let isListeningToChanges = false;
  let selectedTemplate;

  $: shoppingList = $shoppingListStore;
  let templates = [];

  async function startSignal(shoppingListId: string) {
    await startSignalR();
    await startListeningToShoppingListChanges(shoppingListId).then(
      (isConnected) => (isListeningToChanges = isConnected)
    );
  }

  async function stopSignal(shoppingListId: string) {
    if (shoppingList != undefined && !!shoppingListId)
      await stopListeningToShoppingListChanges(shoppingListId).then(
        (isConnected) => (isListeningToChanges = isConnected)
      );
    await stopSignalR();
  }

  window.addEventListener("beforeunload", (event) => {
    if (!!id) {
      stopSignal(id);
    }
  });

  $: {
    if (!!id) {
      startSignal(id);
    }
  }

  onDestroy(async () => {
    await stopSignal(id);
  });

  $: {
    console.log(`Current shopping list is ${id}.`);
    getInitialShoppingList(id);
    getAvailableTemplates();
    apiClient.refreshItemsStoreAsync();
  }

  let newEntryName: string;
  let newEntryQualifier: string;

  async function getInitialShoppingList(id: string) {
    const shoppingListResponse = await apiClient.getShoppingListAsync(id);
    shoppingListStore.set(shoppingListResponse);
  }

  async function getAvailableTemplates() {
    const templatesFromServer = await apiClient.getAvailableTemplatesAsync();
    console.log(templatesFromServer);
    templates = templatesFromServer;
    // select the first template
    if (templatesFromServer.length > 0) {
      selectedTemplate = templates[0].templateId;
    }
  }

  async function applySelectedTemplate() {
    if (selectedTemplate !== undefined) {
      await apiClient.applyTemplate(selectedTemplate, shoppingList.id);
      return;
    }
  }

  async function createEntryAsync() {
    console.log(selectedTemplate);
    if (newEntryName == "") return;

    await apiClient.createEntryWithFreeTextAsync(
      shoppingList.id,
      `${newEntryName} ${newEntryQualifier ?? ""}`
    );
  }

  export let id: string;
</script>

{#if !!shoppingList}
  <div class="flex justify-center items-center text-3xl mb-4">
    <h2 title={shoppingList.id}>Liste {shoppingList.name}</h2>
    <div
      class="connection-status {isListeningToChanges
        ? 'connection-status--connected'
        : 'connection-status--disconnected'}
        ml-4"
    />
  </div>

  <!-- New entry menu that stick at the bottom -->
  <div class="flex border mx-auto w-fit text-center my-10">
    <div>
      <div>
        <input
          class=" text-xl text-center w-max h-[40px] p-2 outline-none"
          placeholder="Produkt"
          bind:value={newEntryName}
        />
      </div>
      <div>
        <input
          class="w-max text-center h-[40px] p-2 outline-none"
          placeholder="Menge"
          bind:value={newEntryQualifier}
        />
      </div>
    </div>
    <div
      class="bg-amber-300 text-center w-20"
      on:click={async () => await createEntryAsync()}
    >
      <div class="text-2xl my-auto">+</div>
    </div>
  </div>


  <div class="grid sm:grid-cols-1 md:grid-cols-3 gap-4 justify-center">

    {#each shoppingList.entries as entry (entry.id)}
      <ShoppingListEntry shoppingListId={shoppingList.id} {entry} />
    {/each}
  </div>

  {#if !!templates && templates.length > 0}
  <div class="mt-10">
    <p class="text-2xl text-center">Rezepte</p>
    <div class="text-center mt-4">
      <span class="mr-4">Rezept</span>
      <select
        class="bg-slate-100 text-center"
        bind:value={selectedTemplate}
        placeholder="nicht"
      >
        <option />
        {#each templates as template (template.templateId)}
          <option value={template.templateId}>{template.name}</option>
        {/each}
      </select>
      <button on:click={applySelectedTemplate} class="ml-4 border p-1"
        >hinzufügen</button
      >
    </div>
    </div>  
  {/if}
{:else}
  Lade Liste ({id}) ...
{/if}

<style>
  .header {
    display: flex;
    align-items: center;
    justify-content: center;
  }
  .connection-status {
    display: block;
    background-color: greenyellow;
    width: 10px;
    height: 10px;
    border-radius: 5px;
    margin-right: 5px;
  }

  .connection-status--connected {
    background-color: greenyellow;
  }

  .connection-status--disconnected {
    background-color: orangered;
  }

  .new-entry {
    position: fixed;
    width: 100%;
    bottom: 0px;
    left: 0px;

    margin: 20px 0px;

    display: flex;
    flex-wrap: wrap;
    justify-content: center;

    text-align: center;
    padding: 5px;
  }

  .new-entry__input {
    height: 30px;

    width: 60vw;
    text-align: center;
    font-size: x-large;
    border: solid;
    border-width: 1px 0px 1px 1px;
    border-radius: 15px 0px 0px 15px;
    flex-shrink: 1;
  }

  .new-entry__add {
    height: 34px;
    padding: 0px 20px;
    font-size: x-large;

    border: solid;
    border-width: 1px 1px 1px 0px;
    border-radius: 0px 15px 15px 0px;

    background-color: greenyellow;
  }

  .bottom-placeholder {
    height: 70px;
    background-color: transparent;
  }

  .items {
  }
</style>
