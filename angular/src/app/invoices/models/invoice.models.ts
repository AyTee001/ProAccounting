export interface CreateInvoiceInput {
    clientId: number;
    date: Date;
    dueDate: Date;
    invoiceItems: EditInvoiceItemInput[];
}

export interface EditInvoiceItemInput {
    id?: number;
    description: string;
    quantity: number;
    productName: string;
    unitPrice: number;
}

export interface InvoiceData {
    id: number;
    clientId: number;
    date: Date;
    dueDate: Date;
    status: InvoiceStatus;
    invoiceItems: InvoiceItemData[];
}

export interface InvoiceItemData {
    id: number;
    invoiceId: number;
    description: string;
    quantity: number;
    productName: string;
    unitPrice: number;
}


export interface UpdateInvoiceInput {
    id: number;
    date: Date;
    dueDate: Date;
    invoiceItems: EditInvoiceItemInput[];
}

export enum InvoiceStatus {
    Unpaid,
    Paid,
}

export const invoiceStatusStringMapping = [
    { value: InvoiceStatus.Unpaid, label: 'Unpaid' },
    { value: InvoiceStatus.Paid, label: 'Paid' },
];

export interface GetAllInvoiceData {
    id: number;
    clientId: number;
    date: Date;
    dueDate: Date;
    status: InvoiceStatus;
    total: number;
}