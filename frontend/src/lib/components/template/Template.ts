export class Template {
    id: string;
    name: string;
    items: TemplateItem[] = [];
    instructions: string;
}

export class TemplateItem {
    id: string;
    name: string;
    amount: string;
}