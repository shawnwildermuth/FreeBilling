import type { Address } from "./address.ts";

export interface Customer {
  id: number;
  companyName: string;
  address: Address | null;
  contact: string | null;
  phoneNumber: string | null;
}