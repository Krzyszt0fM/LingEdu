"use client";

import { useEffect, useState } from "react";
import { AppShell } from "@/components/AppShell";
import { api } from "@/lib/apiClient";

type RankingEntry = { rank: number; userId: string; totalPoints: number };
type UserScore = { userId: string; totalPoints: number };

export default function RankingPage() {
  const [top, setTop] = useState<RankingEntry[]>([]);
  const [me, setMe] = useState<UserScore | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    (async () => {
      try {
        setError(null);
        const [t, m] = await Promise.all([api.rankingTop(10), api.rankingMe()]);
        setTop(t as RankingEntry[]);
        setMe(m as UserScore);
      } catch (err: any) {
        setError(err?.message ?? "Failed to load ranking");
      }
    })();
  }, []);

  return (
    <AppShell>
      <div className="space-y-4">
        <div className="flex items-end justify-between">
          <div>
            <h1 className="text-xl font-semibold">Ranking</h1>
            <p className="text-sm text-[rgb(var(--muted))]">Points from completed exercises.</p>
          </div>

          {me && (
            <div className="text-sm border border-[rgb(var(--border))] rounded-xl px-4 py-2 bg-[rgb(var(--card))]">
              <div className="text-[rgb(var(--muted))] text-xs">My points</div>
              <div className="text-lg font-semibold">{me.totalPoints}</div>
            </div>
          )}
        </div>

        {error && <div className="text-sm text-red-400">{error}</div>}

        <div className="border border-[rgb(var(--border))] rounded-2xl bg-[rgb(var(--card))] overflow-hidden">
          <div className="px-4 py-3 border-b border-[rgb(var(--border))] text-sm text-[rgb(var(--muted))]">
            Top 10
          </div>
          <ul>
            {top.map((x) => (
              <li key={x.userId} className="flex items-center justify-between px-4 py-3 border-b border-[rgb(var(--border))] last:border-b-0">
                <div className="flex items-center gap-3">
                  <div className="w-8 text-[rgb(var(--muted))]">#{x.rank}</div>
                  <div className="text-sm">{x.userId}</div>
                </div>
                <div className="font-semibold">{x.totalPoints}</div>
              </li>
            ))}
          </ul>
        </div>
      </div>
    </AppShell>
  );
}
