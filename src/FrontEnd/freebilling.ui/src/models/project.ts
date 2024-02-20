import { type Customer } from "./customer";

export interface Project {
  id: number;
  projectName: string | null;
  customer: Customer | null;
  startDate: string | null;
  endDate: string | null;
}