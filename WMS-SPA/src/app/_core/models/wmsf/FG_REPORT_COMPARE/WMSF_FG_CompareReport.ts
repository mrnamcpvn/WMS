export interface WMSF_FG_CompareReport {
    factory_ID: string;
    closing_Date: string;
    cdr_No: string;
    location_ID: string;
    order_Status: string;
    pO_WMS_Qty: number;
    pO_ERP_Qty: number;
    update_By: string;
    update_Time: string | null;
}