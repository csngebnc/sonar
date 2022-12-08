import { StatusApiClient } from "@/generated/statusApiClient";

export const statusService = new StatusApiClient(
  import.meta.env.VITE_GATEWAY_BASE_URL
);
