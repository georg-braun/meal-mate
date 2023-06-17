<script lang="ts">
  import apiClient from "../../communication/api/api-client";
  import type { ShoppingListQueryResponseEntry } from "../../communication/api/queries/ShoppingListQueryResponseEntry";
    import ActionButton from "../ActionButton.svelte";

  export let shoppingListId: string;
  export let entry: ShoppingListQueryResponseEntry;
</script>

<div class="grid grid-cols-3 gap-4 my-4 p-2 border rounded-md border-black ">
  <div class="justify-self-end">
    {entry.qualifier}
  </div>
  <div class="text-xl">
    {entry.itemName}
  </div>
  <div>
    <ActionButton
      background="bg-yellow-300"
      placeholder={entry.id}
      action={async () =>
        await apiClient.deleteEntryAsync(shoppingListId, entry.id)}
    >
      Check
      </ActionButton>
  </div>
</div>

<style>
</style>
