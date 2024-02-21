import type { Address } from "./Address.js";

export interface Customer {
  id: number;
  companyName: string;
  address: Address | null;
  contact: string | null;
  phoneNumber: string | null;
}