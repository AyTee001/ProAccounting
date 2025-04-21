export enum LedgerType {
    Asset,
    Revenue
}

export const ledgerTypesStringMapping = [
    { value: LedgerType.Asset, label: 'Assets' },
    { value: LedgerType.Revenue, label: 'Revenue' },
  ];
  

export interface LedgerAccountData {
    id: number,
    code: number,
    name: string,
    ledgerType: LedgerType
}

export interface CreateLedgerAccountInput {
    code: number,
    name: string,
    ledgerType: LedgerType
}

export interface UpdateLedgerAccountInput {
    id: number,
    name: string,
}