<script>
    import { navigate } from "svelte-routing";
    import apiClient from "../../../api/api-client";
    import { Template } from "../../EditTemplate/types/Template";
    import TemplateData from "../../EditTemplate/components/TemplateData.svelte";

    async function handleSubmit(event){
        const modifiedTemplate = event.detail;
        await apiClient.createTemplateAsync(modifiedTemplate);
        navigate('/templates');
    }

    let template = new Template();

</script>
<h1>Neues Rezept</h1>
<TemplateData template={template} on:submit={handleSubmit} />