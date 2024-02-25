import { z } from "zod";

export interface Address {
  id: number;
  addressLine1: string | null;
  addressLine2: string | null;
  addressLine3: string | null;
  city: string | null;
  stateProvince: string | null;
  postalCode: string | null;
  country: string | null;
}

export const AddressSchema = z.object({
  id: z.number(),
  addressLine1: z.nullable(z.string()),
  addressLine2: z.nullable(z.string()),
  addressLine3: z.nullable(z.string()),
  city: z.nullable(z.string()),
  stateProvince: z.nullable(z.string()),
  postalCode: z.nullable(z.string()),
  country: z.nullable(z.string())
});