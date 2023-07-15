<script lang="ts">
  import apiClient from "../../../api/api-client";
  import type { ShoppingListQueryResponseEntry } from "../../../api/queries/ShoppingListQueryResponseEntry";
  import ActionButton from "../ActionButton.svelte";

  export let shoppingListId: string;
  export let entry: ShoppingListQueryResponseEntry;
</script>

<div
  class="pt-2 pb-0 px-2 border border-black text-center min-w-[80px] min-h-[80px]"
  on:click={async () =>
    await apiClient.deleteEntryAsync(shoppingListId, entry.id)}
>
  <p class=" text-xl text-center text-ellipsis">
    {entry.itemName}
  </p>
  <div class="">
    {entry.qualifier}
  </div>
</div>

<style>
</style>
