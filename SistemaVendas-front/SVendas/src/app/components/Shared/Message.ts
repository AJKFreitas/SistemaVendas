export class Message {
    content: string;
    style: string;
    dismissed: boolean = false;

    constructor(content, style?) {
        this.content = content;
        this.style = style || 'info';
    }
}