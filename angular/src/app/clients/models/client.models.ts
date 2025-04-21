export interface ClientData {
    id: number,
    name: string,
    email: string,
    address?: string | null,
    phoneNumber?: string | null
}

export interface CreateClientInput {
    name: string,
    email: string,
    address?: string | null,
    phoneNumber?: string | null
}

export interface UpdateClientInput {
    id: number,
    name: string,
    email: string,
    address?: string | null,
    phoneNumber?: string | null
}