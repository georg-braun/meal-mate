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

  <!-- New entry menu -->
  <div class=" border w-max mx-auto p-4 mb-10">
    Produkt hinzufügen

    <div class="mx-auto mt-4 w-fit text-center">
      <input
        class=" text-xl text-center w-max bg-slate-100 outline-none"
        placeholder="Produkt"
        bind:value={newEntryName}
      />

      <input
        class=" text-xl w-max text-center bg-slate-100 outline-none"
        placeholder="Menge"
        bind:value={newEntryQualifier}
      />

      <button
        class="border boder-black px-2 text-lg bg-slate-700 text-white text-center"
        on:click={async () => await createEntryAsync()}
      >
        +
      </button>
    </div>

    {#if !!templates && templates.length > 0}
      <div class="mt-8">
        <p class="">Produkte von Rezept hinzufügen</p>
        <div class="mt-4">
          <select
            class="bg-slate-100 text-xl text-center"
            bind:value={selectedTemplate}
            placeholder="nicht"
          >
            <option />
            {#each templates as template (template.templateId)}
              <option value={template.templateId}>{template.name}</option>
            {/each}
          </select>
          <button
            on:click={applySelectedTemplate}
            class="border boder-black px-2 text-lg bg-slate-700 text-white text-center">+</button
          >
        </div>
      </div>
    {/if}
  </div>

  <div class="grid sm:grid-cols-1 md:grid-cols-3 gap-4 justify-center">
    {#each shoppingList.entries as entry (entry.id)}
      <ShoppingListEntry shoppingListId={shoppingList.id} {entry} />
    {/each}
  </div>
{:else}
  Lade Liste ({id}) ...
{/if}

<style>
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
</style>
