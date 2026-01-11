"use client";

import { useEffect, useState } from "react";
import { AppShell } from "@/components/AppShell";
import { api } from "@/lib/apiClient";

type Contest = {
  id: string;
  name: string;
  startsAt: string;
  endsAt: string;
  isJoined: boolean;
};

export default function ContestsPage() {
  const [items, setItems] = useState<Contest[]>([]);
  const [error, setError] = useState<string | null>(null);
  const [loadingId, setLoadingId] = useState<string | null>(null);

  async function load() {
    const data = await api.contestsActive();
    setItems(data as Contest[]);
  }

  useEffect(() => {
    (async () => {
      try {
        setError(null);
        await load();
      } catch (err: any) {
        setError(err?.message ?? "Failed to load contests");
      }
    })();
  }, []);

  async function join(id: string) {
    setLoadingId(id);
    try {
      await api.joinContest(id);
      await load();
    } catch (err: any) {
      setError(err?.message ?? "Join failed");
    } finally {
      setLoadingId(null);
    }
  }

  return (
    <AppShell>
      <div className="space-y-4">
        <div>
          <h1 className="text-xl font-semibold">Contests</h1>
          <p className="text-sm text-[rgb(var(--muted))]">Join active challenges to stay consistent.</p>
        </div>

        {error && <div className="text-sm text-red-400">{error}</div>}

        <div className="space-y-2">
          {items.map((c) => (
            <div key={c.id} className="border border-[rgb(var(--border))] rounded-2xl bg-[rgb(var(--card))] p-4 flex items-center justify-between">
              <div>
                <div className="font-medium">{c.name}</div>
                <div className="text-xs text-[rgb(var(--muted))]">
                  {new Date(c.startsAt).toLocaleDateString()} – {new Date(c.endsAt).toLocaleDateString()}
                </div>
              </div>

              {c.isJoined ? (
                <span className="text-sm px-3 py-2 rounded-lg border border-[rgba(34,197,94,0.35)] bg-[rgba(34,197,94,0.10)] text-[rgb(var(--accent-green))]">
                  Joined
                </span>
              ) : (
                <button
                  onClick={() => join(c.id)}
                  disabled={loadingId === c.id}
                  className="text-sm px-3 py-2 rounded-lg border border-[rgba(168,85,247,0.35)] bg-[rgba(168,85,247,0.10)] hover:bg-[rgba(168,85,247,0.18)] disabled:opacity-60"
                >
                  {loadingId === c.id ? "Joining..." : "Join"}
                </button>
              )}
            </div>
          ))}
        </div>
      </div>
    </AppShell>
  );
}
