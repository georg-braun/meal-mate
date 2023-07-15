<script lang="ts">
  import { Link } from "svelte-routing";
  import apiClient from "../../../api/api-client";
  import ActionButton from "../ActionButton.svelte";

  let name: string = "";
  let newListId: string;
</script>

<div class="center">
  <div class="mb-8">
    <input
      bind:value={name}
      class="text-center underline text-xl w-2/3 outline-none"
      placeholder="Name der Liste"
    />
  </div>
  <div>
    <ActionButton
      action={async () =>
        (newListId = await apiClient.createShoppingListAsync(name))}
      background="bg-yellow-300"
    >
      Erstellen
    </ActionButton>
  </div>

  <div class="mt-8">
    {#if !!newListId}
      Eine neue Liste wurde für dich erstellt. Merke dir den Link zu dieser
      Liste!
      <Link to={`/shopping-list/${newListId}`}>Zur Liste</Link>
    {/if}
  </div>
</div>

<style>
  .center {
    text-align: center;
  }
</style>
