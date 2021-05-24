export interface WMSF_FG_CompareReport {
    status: string;
    cdr_No: string;
    model_Name: string;
    article: string;
    pO_Locat_Qty: number | null;
    pO_ERP_Qty: number | null;
    balance: number | null;
    location_ID: string;
    accuracy: number;
}