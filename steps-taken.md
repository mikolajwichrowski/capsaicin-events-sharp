# 0 Architecture
0. Install all packages as described https://docs.microsoft.com/nl-nl/dotnet/core/install/linux-ubuntu
1. Create the project with `dotnet new webapp -o` https://docs.microsoft.com/en-us/aspnet/core/getting-started/?view=aspnetcore-6.0&tabs=macos
2. Start the app with `dotnet watch run` to test.
  - Issue: got error when running "System.ComponentModel.Win32Exception (2): An error occurred trying to start process '/run/containerd/io.containerd.runtime.v2.task/k8s.io/8bd9abe88696590c1c5499b951133133513e3a3f023e22ff94efeeba78b457d0/rootfs/usr/share/dotnet/dotnet' with working directory '/workspace/capsaicin-events-sharp'. No such file or directory"
  - Solution: run `sudo rm -rf /usr/share/dotnet` to remove the current installation of dotnet and then install using `curl -fsSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 6.0 --install-dir /usr/share/dotnet`. Then set the correct dotnet location in the env variables so that dotnet knows where the executable is located `export DOTNET_ROOT=/usr/share/dotnet/dotnet && export PATH=$PATH:/usr/share/dotnet/dotnet`
