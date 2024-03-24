import { z } from "zod";
import { type Customer } from "./Customer";

export interface Project {
  id: number;
  projectName: string | null;
  customer: Customer | null;
  customerId: Number | null;
  startDate: string | null;
  endDate: string | null;
}

export const ProjectSchema = z.object({
  id: z.number(),
  projectName: z.string().min(1, "Required"),
  customerId: z.number().min(0, "Required");
  startDate: z.nullable(z.date()),
  endDate: z.nullable(z.date())
});

export function generateEmptyProject() {
  return {
    id: 0,
    projectName: "",
    customerId: 0,
    customer: null,
    startDate: new Date().toLocaleDateString(),
    endDate: null
  } as Project;
}

export type ProjectErrors = z.inferFormattedError<typeof ProjectSchema>;