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
  addressLine1: z.string().min(5, "Required to be at least five characters."),
  addressLine2: z.nullable(z.string()),
  addressLine3: z.nullable(z.string()),
  city: z.string().min(1, "Required"),
  stateProvince: z.string().min(1, "Required"),
  postalCode: z.string().min(1, "Required"),
  country: z.nullable(z.string())
});

export function generateEmptyAddress() {
  return {
    id: 0,
    addressLine1: "",
    addressLine2: "",
    addressLine3: "",
    city: "",
    stateProvince: "",
    postalCode: "",
    country: ""
  }
}