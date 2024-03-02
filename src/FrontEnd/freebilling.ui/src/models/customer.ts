import { type Address, AddressSchema } from "./Address.js";
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
  companyName: z.string(),
  address: z.nullable(AddressSchema),
  contact: z.nullable(z.string()),
  phoneNumbe: z.nullable(z.string())
});

export type CustomerErrors = z.inferFormattedError<typeof CustomerSchema>;