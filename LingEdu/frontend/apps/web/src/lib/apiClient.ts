import { createApiClient, type TokenStore } from "@lingedu/api";

const tokenStore: TokenStore = {
  async get() {
    if (typeof window === "undefined") return null;
    return localStorage.getItem("token");
  },
  async set(token) {
    if (typeof window === "undefined") return;
    if (!token) localStorage.removeItem("token");
    else localStorage.setItem("token", token);
  },
};

const baseUrl = process.env.NEXT_PUBLIC_API_BASE_URL;

if (!baseUrl) {
  throw new Error("NEXT_PUBLIC_API_BASE_URL is missing");
}

export const api = createApiClient({ baseUrl, tokenStore });
