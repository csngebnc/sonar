import { ShippingApiClient } from "@/generated/shippingApiClient";

export const shippingService = new ShippingApiClient(
  import.meta.env.VITE_GATEWAY_BASE_URL
);
