import { type Address, AddressSchema, generateEmptyAddress } from "./Address.js";
import { z } from "zod";

export interface Customer {
  id: number;
  companyName: string;
  address: Address | null;
  contact: string | null;
  phoneNumber: string | null;
}

export const CustomerSchema = z.object({
  id: z.number(),
  companyName: z.string().min(1, "Required"),
  address: z.nullable(AddressSchema),
  contact: z.nullable(z.string()),
  phoneNumber: z.nullable(z.string())
});

export function generateEmptyCustomer() {
  return {
    id: 0,
    companyName: "",
    contact: "",
    phoneNumber: "",
    address: generateEmptyAddress()
  } as Customer;
}

export type CustomerErrors = z.inferFormattedError<typeof CustomerSchema>;