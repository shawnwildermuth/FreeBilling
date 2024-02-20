import { type Employee } from "./employee";
import { type Project } from "./project";

export interface Ticket {
  id: number;
  employee: Employee | null;
  hours: number;
  billingRate: number;
  date: string | null;
  project: Project | null;
  workPerformed: string | null;
}