import { type Customer } from "./Customer";

export interface Project {
  id: number;
  projectName: string | null;
  customer: Customer | null;
  startDate: string | null;
  endDate: string | null;
}