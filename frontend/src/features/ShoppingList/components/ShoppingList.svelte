<script lang="ts">
  import { onDestroy } from "svelte";

  import {
    startListeningToShoppingListChanges,
    startSignalR,
    stopListeningToShoppingListChanges,
    stopSignalR,
  } from "../../../lib/communication/hub/mealMateHub";
  import { shoppingListStore } from "../../../lib/store";
  import ShoppingListEntry from "./ShoppingListEntry.svelte";
  import apiClient from "@/api/api-client";

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

  window.addEventListener("freeze", (event) => {
    console.log("tab is in freeze mode")
    if (!!id) {
      stopSignal(id);
    }
  });

  window.addEventListener("resume", (event) => {
    console.log("tab is in resume mode")
    if (!!id) {
      startSignal(id);
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
  <p class="text-center text-2xl" title={shoppingList.id}>
    Liste {shoppingList.name}
  </p>

  {#if isListeningToChanges}
    <div class="border rounded text-xs w-fit px-2 bg-teal-400 mx-auto">
      Verbunden
    </div>
  {:else}
    <div class="border rounded text-xs w-fit px-2 bg-amber-300 mx-auto">
      Nicht verbunden
    </div>
  {/if}

  <!-- New entry menu -->
  <div class="border border-black w-max mx-auto p-4 mb-10 mt-4">
    Produkt hinzufügen

    <div class="mt-4 text-center">
      <div>
        <input
          class=" text-xl text-center bg-slate-100 outline-none"
          placeholder="Produkt"
          bind:value={newEntryName}
        />
      </div>
      <div>
        <input
          class="text-xl text-center bg-slate-100 outline-none"
          placeholder="Menge"
          bind:value={newEntryQualifier}
        />
      </div>
      <div>
        <button
          class="border boder-black px-2 text-lg bg-slate-700 mt-2 text-white text-center"
          on:click={async () => await createEntryAsync()}
        >
          hinzufügen
        </button>
      </div>
    </div>

    {#if !!templates && templates.length > 0}
      <div class="mt-8">
        <p class="">Produkte von Rezept hinzufügen</p>
        <div class="mt-4 text-center">
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
        </div>
        <div class="text-center">
          <button
            on:click={applySelectedTemplate}
            class="border boder-black px-2 text-lg bg-slate-700 mt-2 text-white text-center"
            >hinzufügen</button
          >
        </div>
      </div>
    {/if}
  </div>

  <div class="flex flex-wrap gap-4 justify-center">
    {#each shoppingList.entries as entry (entry.id)}
      <ShoppingListEntry shoppingListId={shoppingList.id} {entry} />
    {/each}
  </div>
{:else}
  Lade Liste ({id}) ...
{/if}

<style>
</style>
