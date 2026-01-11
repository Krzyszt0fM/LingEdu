"use client";

import Link from "next/link";
import { usePathname, useRouter } from "next/navigation";

function NavLink({ href, label }: { href: string; label: string }) {
  const pathname = usePathname();
  const active = pathname === href || pathname.startsWith(href + "/");

  return (
    <Link
      href={href}
      className={[
        "px-3 py-2 rounded-lg text-sm border",
        active
          ? "border-[rgb(var(--accent-blue))] bg-[rgba(99,102,241,0.12)]"
          : "border-[rgb(var(--border))] hover:bg-white/5",
      ].join(" ")}
    >
      {label}
    </Link>
  );
}

export function AppShell({ children }: { children: React.ReactNode }) {
  const router = useRouter();

  function logout() {
    localStorage.removeItem("token");
    router.push("/login");
  }

  return (
    <div className="min-h-screen">
      <header className="sticky top-0 z-50 border-b border-[rgb(var(--border))] bg-[rgb(var(--bg))]/90 backdrop-blur">
        <div className="max-w-5xl mx-auto px-6 py-4 flex items-center justify-between">
          <div className="flex items-center gap-3">
            <div className="h-9 w-9 rounded-xl bg-[rgba(168,85,247,0.15)] border border-[rgb(var(--border))]" />
            <div>
              <div className="font-semibold leading-tight">LingEdu</div>
              <div className="text-xs text-[rgb(var(--muted))]">Learn daily. Track progress.</div>
            </div>
          </div>

          <nav className="flex items-center gap-2">
            <NavLink href="/exercises" label="Exercises" />
            <NavLink href="/ranking" label="Ranking" />
            <NavLink href="/reports" label="Report" />
            <NavLink href="/contests" label="Contests" />

            <button
              onClick={logout}
              className="ml-2 px-3 py-2 rounded-lg text-sm border border-[rgb(var(--border))] hover:bg-white/5"
            >
              Logout
            </button>
          </nav>
        </div>
      </header>

      <main className="max-w-5xl mx-auto px-6 py-6">
        {children}
      </main>
    </div>
  );
}
