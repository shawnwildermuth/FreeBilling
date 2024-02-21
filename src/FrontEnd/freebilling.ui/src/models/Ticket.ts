import { type Employee } from "./Employee";
import { type Project } from "./Project";

export interface Ticket {
  id: number;
  employee: Employee | null;
  hours: number;
  billingRate: number;
  date: string | null;
  project: Project | null;
  workPerformed: string | null;
}