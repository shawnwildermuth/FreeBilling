import type { Address } from "./Address";
import type { ProjectModel } from "./ProjectModel";

export interface CustomerDetails {
  id: number;
  companyName: string;
  address: Address | null;
  contact: string | null;
  phoneNumber: string | null;
  totalBalance: number;
  projects: ProjectModel[] | null;
}