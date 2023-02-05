<script lang="ts">
    import {createEventDispatcher} from "svelte"
	const dispatch = createEventDispatcher();

    let itemTitle: string = "";

    function add() {
        console.log("add")
        dispatch("add-new-entry", {
            ItemTitle: itemTitle
        })

    }
</script>

<div class="entry"> 
    <input class="entry__input" bind:value={itemTitle} />
    <button class="entry__add" on:click={add}>+</button>
</div>



<style>
    .entry {
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;

        width: 100px;
        height: 100px;
        background-color: green;
    }

    .entry__input{
        border: none;
        width: 80px;
        background-color: transparent;
        border-bottom: solid;
        border-width: 1px;
        text-align: center;
    }

    .entry__add{
        font-size: smaller;
         margin-top: 5px;
        padding: 2px;
    }

</style>
