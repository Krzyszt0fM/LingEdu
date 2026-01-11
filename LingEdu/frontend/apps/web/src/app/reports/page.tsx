"use client";

import { useEffect, useState } from "react";
import { AppShell } from "@/components/AppShell";
import { api } from "@/lib/apiClient";

type Report = {
  userId: string;
  totalPoints: number;
  completedExercisesCount: number;
  lastCompletedAt: string | null;
  recentProgress: Array<{ exerciseId: string; points: number; isCorrect: boolean; completedAt: string }>;
};

export default function ReportsPage() {
  const [report, setReport] = useState<Report | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    (async () => {
      try {
        setError(null);
        const data = await api.reportMe();
        setReport(data as Report);
      } catch (err: any) {
        setError(err?.message ?? "Failed to load report");
      }
    })();
  }, []);

  return (
    <AppShell>
      <div className="space-y-4">
        <div>
          <h1 className="text-xl font-semibold">My report</h1>
          <p className="text-sm text-[rgb(var(--muted))]">Progress summary and recent activity.</p>
        </div>

        {error && <div className="text-sm text-red-400">{error}</div>}

        {report && (
          <>
            <div className="grid grid-cols-1 md:grid-cols-3 gap-3">
              <Stat title="Total points" value={report.totalPoints} accent="blue" />
              <Stat title="Completed" value={report.completedExercisesCount} accent="green" />
              <Stat title="Last activity" value={report.lastCompletedAt ? new Date(report.lastCompletedAt).toLocaleString() : "—"} accent="violet" />
            </div>

            <div className="border border-[rgb(var(--border))] rounded-2xl bg-[rgb(var(--card))] overflow-hidden">
              <div className="px-4 py-3 border-b border-[rgb(var(--border))] text-sm text-[rgb(var(--muted))]">
                Recent progress (last 20)
              </div>
              <ul>
                {report.recentProgress.map((x, i) => (
                  <li key={x.exerciseId + i} className="px-4 py-3 border-b border-[rgb(var(--border))] last:border-b-0">
                    <div className="flex items-center justify-between">
                      <div className="text-sm">
                        <div className="text-[rgb(var(--muted))] text-xs">Exercise</div>
                        <div className="truncate">{x.exerciseId}</div>
                      </div>
                      <div className="text-right">
                        <div className="text-[rgb(var(--muted))] text-xs">Points</div>
                        <div className="font-semibold">{x.points}</div>
                      </div>
                    </div>

                    <div className="mt-2 flex items-center justify-between text-xs text-[rgb(var(--muted))]">
                      <div>{new Date(x.completedAt).toLocaleString()}</div>
                      <div className={x.isCorrect ? "text-[rgb(var(--accent-green))]" : "text-red-400"}>
                        {x.isCorrect ? "Correct" : "Incorrect"}
                      </div>
                    </div>
                  </li>
                ))}
              </ul>
            </div>
          </>
        )}
      </div>
    </AppShell>
  );
}

function Stat({ title, value, accent }: { title: string; value: any; accent: "blue" | "green" | "violet" }) {
  const accentClass =
    accent === "blue"
      ? "border-[rgba(99,102,241,0.35)] bg-[rgba(99,102,241,0.10)]"
      : accent === "green"
      ? "border-[rgba(34,197,94,0.35)] bg-[rgba(34,197,94,0.10)]"
      : "border-[rgba(168,85,247,0.35)] bg-[rgba(168,85,247,0.10)]";

  return (
    <div className={`border rounded-2xl p-4 ${accentClass}`}>
      <div className="text-xs text-[rgb(var(--muted))]">{title}</div>
      <div className="text-lg font-semibold mt-1">{value}</div>
    </div>
  );
}
