"use client";

import { useEffect, useState } from "react";
import { api } from "@/lib/apiClient";
import Link from "next/link";
import { AppShell } from "@/components/AppShell";


type ExerciseDto = {
  id: string;
  title: string;
  type: string;
  level: string;
  isPremium: boolean;
};

export default function ExercisesPage() {
  const [items, setItems] = useState<ExerciseDto[]>([]);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    (async () => {
      try {
        const data = await api.exercisesForToday();
        setItems(data as ExerciseDto[]);
      } catch (err: any) {
        setError(err?.message ?? "Failed to load exercises");
      }
    })();
  }, []);

  return (
    <AppShell>
    <main className="min-h-screen p-6 max-w-3xl mx-auto space-y-4">
      <div className="flex items-center justify-between">
        <h1 className="text-xl font-semibold">Exercises for today</h1>
        <Link className="underline" href="/login">Logout</Link>
      </div>

      {error && <div className="text-sm text-red-600">{error}</div>}

      <ul className="space-y-2">
        {items.map((x) => (
          <li key={x.id} className="border rounded-xl p-4 flex items-center justify-between">
            <div>
              <div className="font-medium">{x.title}</div>
              <div className="text-sm opacity-70">
                {x.level} • {x.type} {x.isPremium ? "• Premium" : ""}
              </div>
            </div>
            <Link className="underline" href={`/exercises/${x.id}`}>Open</Link>
          </li>
        ))}
      </ul>
    </main>
    </AppShell>
  );
}
