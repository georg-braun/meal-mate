<script lang="ts">
  import apiClient from "../../../api/api-client";
  import type { ItemsQueryResponse } from "../../../api/queries/ItemsQueryResponse";
  import ActionButton from "../../../lib/components/ActionButton.svelte";

  export let item: ItemsQueryResponse;
</script>

<div class="grid grid-cols-3 my-2 hover:bg-slate-100" title={item.id}>
<!--   <div>
    {item.categoryName ?? "Unbekannte Kategorie"}
  </div> -->
  <div class="col-span-2">
    {item.name}
  </div>
  <div class="text-center">
    <ActionButton
      background="bg-red-300"
      action={async () => await apiClient.deleteItemAsync(item.id)}
      >Löschen</ActionButton
    >
  </div>
</div>
