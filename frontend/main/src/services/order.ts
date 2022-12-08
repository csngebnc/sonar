import { OrderApiClient } from "@/generated/orderApiClient";

export const orderService = new OrderApiClient(
  import.meta.env.VITE_GATEWAY_BASE_URL
);
