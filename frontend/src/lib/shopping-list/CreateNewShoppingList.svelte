<script>
    import { createShoppingList } from "../communication/commands";
    import { currentUser } from "../communication/pocketbase";

    let title;

    async function create() {
        createShoppingList(title, $currentUser.id);
    }
</script>

<span>Neue Liste:</span>
<input bind:value={title} />
<button on:click={create}>Erstellen</button>
