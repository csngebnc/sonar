import { PartnerApiClient } from "@/generated/partnerApiClient";

export const partnerService = new PartnerApiClient(
  import.meta.env.VITE_GATEWAY_BASE_URL
);
