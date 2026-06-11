# DbDynamicApi



![GitHub Stars](https://img.shields.io/github/stars/CreatorXuYuFei/DbDynamicApi?style=social)



![GitHub Forks](https://img.shields.io/github/forks/CreatorXuYuFei/DbDynamicApi?style=social)



![GitHub License](https://img.shields.io/github/license/CreatorXuYuFei/DbDynamicApi)



![NuGet Version](https://img.shields.io/nuget/v/DbDynamicApi.svg)



![Build Status](https://img.shields.io/github/actions/workflow/status/CreatorXuYuFei/DbDynamicApi/ci.yml?branch=main)



![Code Quality](https://img.shields.io/codefactor/grade/github/CreatorXuYuFei/DbDynamicApi/main)

> 🔥 企业级低代码数据库访问 API 服务，基于
>
> `NewLife.XCode`
>
> 与自研插件
>
> `Enterprise.Configuration`
>
> `CoreLogger`
>
> 封装，零代码生成标准化 RESTful API，内置完整企业级治理能力。

[English](README.en.md) | [简体中文](README.md)

## 📖 项目简介

`DbDynamicApi` 是一款面向企业级场景的通用数据库访问操作 API 服务，基于后端核心技术栈 `NewLife.XCode`、`Enterprise.Configuration`、`CoreLogger` [(56)](https://www.gitlink.org.cn/NewLife/XCode)构建。

项目的核心设计目标是**消除重复的数据层接口开发工作**：开发者仅需编写标准 SQL 语句、配置接口规则，即可快速生成符合 RESTful 规范的标准化后端接口，无需编写业务逻辑代码，同时原生提供企业级应用所需的安全、高性能、可观测、热更新、多环境治理能力。

本项目完全兼容.NET 8 LTS 跨平台能力，支持独立部署、容器化部署、Kubernetes 集群部署，可无缝对接主流关系型数据库，覆盖微服务、中台开发、低代码平台、临时数据接口开放等典型业务[(134)](https://kitemetric.com/blogs/dynamic-database-api-in-net-9-clean-architecture-mediatr-for-ultimate-flexibility)场景。

## ✨ 核心特性

### 🚀 零代码 / 低代码 API 生成



* 编写标准 SQL 配置，即可自动生成符合 RESTful 规范的完整 HTTP 接口，无需编写后端业务代码。

* 原生支持 MyBatis 风格的动态 SQL 语法，包括`#{}`/`${}`参数占位符、`<where>`/`<if>`/`<foreach>`等动态标签，适配多条件查询、批量操作、复杂业务组[(158)](https://www.51dbapi.com/)装场景。

* 完整支持数据库表的新增、查询、修改、删除操作，可通过配置单独启用 / 禁用指定操作，自动生成标准化 CRUD 接口。

* 支持多 SQL 组合执行，一次请求可完成多表查询、多数据组装，减少前端请求次数。

### 🗃️ 多数据源统一治理



* 兼容所有支持 JDBC/[ADO.NET](https://ADO.NET)协议的主流关系型数据库，包括 MySQL、SQL Server、PostgreSQL、Oracle、达梦、金仓、瀚高等主流及国产数据库。

* 支持多数据源动态配置、切换，可根据接口规则绑定不同数据库实例，适配分库分表、多业务数据源隔离、跨库联合[(158)](https://www.51dbapi.com/)查询场景。

* 基于`NewLife.XCode`实现精细化数据库连接池管理，支持最大连接数、空闲连接数、连接超时时间、回收机制配置，有效控制数据库连接开销。

### ⚙️ 企业级配置治理



* 基于自研组件`Enterprise.Configuration`实现多配置源优先级管理，支持 JSON 配置文件、环境变量、命令行参数、内存配置四类配置源。

* 采用**后加入优先覆盖**的优先级规则，天然适配开发、测试、预发布、生产多环境配置隔离，敏感配置（数据库账号、密钥）可通过环境变量 / 命令行参数传入，避免明文存储在配置文件中。

* 原生支持配置热更新：监听配置文件变更或配置中心推送消息后，无需重启服务即可实时刷新数据源连接、API 规则、缓存配置、权限配置，实现无感知配置更新。

* 支持强类型配置绑定、配置项合法性校验，启动 /reload 时自动校验所有规则，避免非法配置导致的服务异常。

### 🚄 高性能流量优化



* 内置多级结果集缓存，支持内存缓存、Redis 分布式缓存两种存储类型，可自定义缓存 Key 生成规则、自动过期时长、主动刷新策略，重复查询请求直接读取缓存，减少数据库 IO。

* 基于`NewLife.XCode`的 SQL 参数化防护，彻底避免 SQL 注入攻击，同时优化执行计划，提升 SQ[(56)](https://www.gitlink.org.cn/NewLife/XCode)L 执行效率。

* 内置大数据量分页优化、流式传输、N+1 查询问题防御，处理大结果集时不会占用大量服务内存，保障接口稳定性。

* 支持接口级别的请求限流、防抖，可配置每秒 / 每分钟请求阈值，避免数据库被过度调用。

### 🛡️ 完整安全防护能力



* 接口级权限管控：支持 AppKey 签名校验、角色权限隔离、接口访问有效期限定，未授权请求直接拦截。

* 网络访问控制：内置 IP 白名单 / 黑名单机制，可限定接口的访问来源 IP 段，仅允许信任的网络地址调用。

* 数据安全防护：自动对 SQL 参数进行化防护，阻断 SQL 注入攻击；支持配置敏感数据脱敏规则，在接口返回结果中模糊处理手机号、身份证号、银行卡号等隐私字段。

* 请求合规校验：强制校验请求参数格式、类型、长度、枚举值范围，不符合规范的请求提前拦截，避免无效 SQL 执行。

### 📊 标准化可观测能力



* 自动集成 Swagger/OpenAPI 3.0 标准文档，支持在线接口调试、自动生成多语言客户端调用示例、导出标准接口文档。

* 基于自研组件`CoreLogger`实现全链路日志追踪，完整记录请求入参、实际执行 SQL、数据库耗时、接口响应结果、错误堆栈，支持日志按时间 / 文件大小分片、异步写入、多目标输出（控制台、本地文件、ELK、Loki 等集中式日志系统）。

* 内置 Prometheus metrics 端点，可对接 Grafana 实现可视化监控，实时展示接口请求量、错误率、数据库耗时、缓存命中率、内存占用等核心运维指标。

* 支持链路追踪 ID 透传，可将接口请求、SQL 执行、缓存操作的全链路日志串联，快速定位性能瓶颈或异常问题。

### 🔌 灵活扩展能力



* 采用插件化架构设计，支持自定义参数处理逻辑、全局响应格式转换、新增数据源驱动、自定义告警规则、多语言国际化返回格式。

* 提供 SPI 机制扩展点，无需修改核心源码，即可实现业务定制能力，例如自动添加租户 ID 过滤、数据权限校验、结果集二次加工。

* 支持自定义响应封装规则，可按照业务前端要求统一返回格式，自定义成功 / 失败码、多语言提示信息。

## 🏗️ 技术栈与依赖

本项目基于以下核心组件构建，所有依赖均为长期维护的稳定版本：



| 依赖项名称                           | 版本             | 核心作用                                                                  | 开源仓库                                                                                   |
| ------------------------------- | -------------- | --------------------------------------------------------------------- | -------------------------------------------------------------------------------------- |
| NewLife.XCode                   | 11.27.2026.601 | 高性能.NET ORM 框架，提供数据库连接池管理、SQL 参数化防注入、实体映射、一级 / 二级缓存、分表分库、自动建模、数据库自动迁移 | [NewLife.XCode](https://github.com/NewLifeX/NewLife.XCode)                             |
| Enterprise.Configuration        | 2.1.1          | 自研企业级配置管理组件，提供多配置源优先级管理、热更新监听、强类型配置绑定、配置校验                            | [Enterprise.Configuration](https://github.com/CreatorXuYuFei/Enterprise.Configuration) |
| CoreLogger                      | 2.1.0          | 自研高性能结构化日志组件，支持异步分片写入、多目标输出、链路追踪，完整记录 API 请求与数据库执行细节                  | [CoreLogger](https://github.com/CreatorXuYuFei/CoreLogger)                             |
| [ASP.NET](https://ASP.NET) Core | .NET 8 LTS     | 跨平台 Web 宿主框架，提供 HTTP 请求处理、路由、依赖注入、中间件能力                               | [ASP.NET Core](https://github.com/dotnet/aspnetcore)                                   |
| Swashbuckle.AspNetCore          | 6.5.0          | 集成 Swagger/OpenAPI 3.0 标准文档，提供在线调试能力                                  | [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)    |
| Docker                          | 最新版            | 容器化部署支持，提供标准 Dockerfile、docker-compose.yml 配置                         | [Docker](https://github.com/docker)                                                    |

## 👥 目标用户与典型场景

### 1. 后端开发人员

快速实现数据层接口，减少重复编写 DAL、Controller、SQL 代码的工作量，统一规范数据层接口标准，避免不同开发人员实现的接口风格差异，专注于业务逻辑开发而非通用数据接口开发。

### 2. BI / 数据分析师

直接通过 SQL 生成大屏、报表、可视化需要的后端数据接口，无需依赖开发人员，自助完成数据对接，实时获取业务分析数据，缩短数据可视化的开发周期。

### 3. 运维工程师

快速发布临时数据查询接口，用于线上问题排查、业务数据临时导出；可设置接口有效期、IP 白名单、请求限流，保障临时接口安全；统一管理所有数据接口，集中治理数据出口权限。

### 4. 企业级中台开发团队

统一管理公司所有数据访问接口，实现数据源、API 规则、权限、日志、缓存的集中治理，形成统一的数据服务中台，规范公司数据出口管理，避免业务侧直接连接数据库导致的安全风险。

### 5. 低代码平台开发者

作为数据接口层的底层支撑组件，为低代码平台提供动态数据接口生成能力，配合低代码组件实现数据双向绑定、实时数据展示、表单数据提交，快速搭建低代码业务页面。

## 🚀 快速开始

### 📋 前置条件

在启动项目前，需确保宿主机 / 目标部署环境已安装以下依赖：



1. [.NET 8 SDK/runtime](https://dotnet.microsoft.com/download/dotnet/8.0) 及以上版本，支持跨平台部署 Windows/Linux/macOS。

2. 目标数据库实例：MySQL、SQL Server、PostgreSQL、Oracle 等主流关系型数据库，且具备合法的连接权限、目标数据库的读写权限。

3. （可选）[Docker](https://docs.docker.com/get-docker/)、[Docker Compose](https://docs.docker.com/compose/install/)，用于容器化部署。

4. （可选）[kubectl](https://kubernetes.io/docs/tasks/tools/) 及 Kubernetes 集群访问权限，用于集群化部署。

### 📦 安装方式

#### 方式一：NuGet 安装（推荐）

执行以下命令，通过 NuGet 包管理器快速安装组件：



```
dotnet add package DbDynamicApi
```

#### 方式二：源码安装



1. 克隆代码仓库到本地：



```
git clone https://github.com/CreatorXuYuFei/DbDynamicApi.git

cd DbDynamicApi
```



1. 执行构建命令，生成发布包：



```
dotnet build -c Release

dotnet publish -c Release -o ./publish
```

### 🔧 基础配置示例

在项目根目录下添加 `appsettings.json` 配置文件，完整配置示例如下：



```
{

 "Logging": {

   "LogLevel": {

     "Default": "Information",

     "Microsoft.AspNetCore": "Warning"

   },

   "CoreLogger": {

     "Enable": true,

     "Path": "logs",

     "MaxFileSize": 10485760,

     "FlushMode": "Async",

     "Targets": \[ "Console", "File" ]

   }

 },

 "DbDynamicApi": {

   "DataSources": \[

     {

       "Name": "Default",

       "ConnectionString": "Server=localhost;Database=testdb;Uid=root;Pwd=123456;AllowUserVariables=True;",

       "DbType": "MySql",

       "IsDefault": true,

       "Pooling": true,

       "MaxPoolSize": 100,

       "MinPoolSize": 5,

       "ConnectTimeout": 30000

     }

   ],

   "ApiRules": \[

     {

       "Name": "GetUserList",

       "Route": "/api/user/list",

       "Method": "GET",

       "DataSource": "Default",

       "Sql": "SELECT \* FROM user WHERE status = #{status} AND create\_time >= #{createTime}",

       "Enable": true,

       "Authorize": true,

       "RateLimit": "100/minute",

       "Cache": {

         "Enable": true,

         "Key": "user\_list\_#{status}\_#{createTime}",

         "Expire": 300,

         "Type": "Memory"

       }

     },

     {

       "Name": "AddUser",

       "Route": "/api/user/add",

       "Method": "POST",

       "DataSource": "Default",

       "Sql": "INSERT INTO user (name, phone, status) VALUES (#{name}, #{phone}, #{status})",

       "Enable": true,

       "Authorize": true,

       "RateLimit": "50/minute",

       "Cache": {

         "Enable": false

       }

     }

   ],

   "Security": {

     "IpWhitelist": \[ "127.0.0.1", "192.168.1.0/24" ],

     "AppKey": "your-valid-app-key-here",

     "EnableSqlInjectFilter": true,

     "SensitiveFields": \[ "phone", "email" ]

   },

   "Cache": {

     "DefaultType": "Memory",

     "RedisConnectionString": "localhost:6379,password=123456,defaultDatabase=0"

   },

   "HotReload": {

     "Enable": true,

     "NotifyUrl": "http://localhost:5000/admin/reload",

     "ApiKey": "your-reload-api-key"

   }

 }

}
```

### ▶️ 启动服务

#### 1. 原生启动

执行以下命令，直接启动服务：



```
dotnet run --project DbDynamicApi --urls http://0.0.0.0:5000
```

#### 2. Docker 启动

使用项目根目录下的 `docker-compose.yml` 配置文件，快速构建镜像并启动容器：



```
docker-compose up -d --build
```

#### 3. 验证服务启动成功

打开浏览器，访问 Swagger 文档地址：`http://localhost:5000/swagger`，即可看到所有配置的接口列表，支持在线调试接口。

### 📩 调用接口示例

使用 `curl` 或 Postman 调用生成的接口，示例如下：



```
curl -X GET "http://localhost:5000/api/user/list?status=1\&createTime=2023-01-01" \\

 -H "Authorization: Bearer your-valid-app-key-here" \\

 -H "Content-Type: application/json"
```

正常情况下，将返回标准化的 JSON 格式数据，包含数据库查询结果。

## 📚 详细使用文档

### 🔨 完整配置说明

#### 1. 数据源配置

`DataSources` 节点用于配置数据库连接信息，支持多数据源配置，核心配置项说明如下：



| 配置项名称              | 说明                                                                    | 必填 | 默认值     |
| ------------------ | --------------------------------------------------------------------- | -- | ------- |
| `Name`             | 数据源唯一标识，与`ApiRules.DataSource`绑定                                      | 是  | 无       |
| `ConnectionString` | 数据库连接字符串，需包含账号、密码、数据库地址、库名                                            | 是  | 无       |
| `DbType`           | 数据库类型，支持`MySql`/`SqlServer`/`PostgreSQL`/`Oracle`/`Dameng`/`Kingbase` | 是  | 无       |
| `IsDefault`        | 是否为默认数据源                                                              | 否  | `false` |
| `Pooling`          | 是否启用数据库连接池                                                            | 否  | `true`  |
| `MaxPoolSize`      | 连接池最大连接数                                                              | 否  | 100     |
| `MinPoolSize`      | 连接池最小空闲连接数                                                            | 否  | 5       |
| `ConnectTimeout`   | 数据库连接超时时间（毫秒）                                                         | 否  | 30000   |

#### 2. API 规则配置

`ApiRules` 节点用于配置生成的接口规则，核心配置项说明如下：



| 配置项名称          | 说明                                   | 必填 | 默认值      |
| -------------- | ------------------------------------ | -- | -------- |
| `Name`         | 接口唯一名称，用于日志、监控标识                     | 是  | 无        |
| `Route`        | 接口路由地址，标准 RESTful 格式                 | 是  | 无        |
| `Method`       | 接口请求方法，支持`GET`/`POST`/`PUT`/`DELETE` | 是  | `GET`    |
| `DataSource`   | 绑定的数据源标识，与`DataSources.Name`对应       | 是  | 无        |
| `Sql`          | 标准 SQL 语句，支持 MyBatis 风格动态 SQL 占位符    | 是  | 无        |
| `Enable`       | 是否启用该接口                              | 否  | `true`   |
| `Authorize`    | 是否启用权限校验（AppKey+IP 白名单）              | 否  | `true`   |
| `RateLimit`    | 接口限流规则，格式：`次数/时间单位`                  | 否  | 无        |
| `Cache.Enable` | 是否启用接口结果缓存                           | 否  | `false`  |
| `Cache.Key`    | 缓存 Key 模板，支持`#{}`参数占位符               | 否  | 无        |
| `Cache.Expire` | 缓存过期时长（秒）                            | 否  | 300      |
| `Cache.Type`   | 缓存存储类型，支持`Memory`/`Redis`            | 否  | 全局配置默认类型 |

#### 3. 安全配置

`Security` 节点用于配置全局安全规则，核心配置项说明如下：



| 配置项名称                   | 说明                                                  | 必填 | 默认值        |
| ----------------------- | --------------------------------------------------- | -- | ---------- |
| `IpWhitelist`           | 允许调用的 IP 白名单，支持 IP 段格式                              | 否  | 空（允许所有 IP） |
| `AppKey`                | 接口调用的授权密钥，请求头`Authorization: Bearer <AppKey>`需携带合法值 | 是  | 无          |
| `EnableSqlInjectFilter` | 是否启用 SQL 注入参数化防护                                    | 否  | `true`     |
| `SensitiveFields`       | 接口返回结果中需要脱敏的字段列表                                    | 否  | 空          |

#### 4. 热更新配置

`HotReload` 节点用于配置热更新规则，核心配置项说明如下：



| 配置项名称       | 说明                       | 必填 | 默认值     |
| ----------- | ------------------------ | -- | ------- |
| `Enable`    | 是否启用配置热更新                | 否  | `false` |
| `NotifyUrl` | 配置变更通知接收地址，配置中心推送消息的回调地址 | 否  | 无       |
| `ApiKey`    | 热更新接口的调用密钥，防止未授权调用热更新接口  | 是  | 无       |

### 🔄 配置热更新验证



1. 修改`appsettings.json`配置文件，例如调整接口限流规则、缓存过期时长、IP 白名单内容。

2. 保存配置文件后，无需重启服务，项目将自动检测到配置变更，或通过调用`NotifyUrl`接收配置中心推送的变更消息。

3. 查看服务日志，将输出`Configuration reloaded successfully`的确认日志，标识配置已刷新生效。

4. 重新调用对应接口，验证修改后的配置已生效。

### 🚢 多环境配置示例

通过**环境变量覆盖**的方式区分多环境配置，以生产环境为例：



1. 设置环境变量，覆盖配置文件中的敏感配置：



```
export DbDynamicApi\_\_DataSources\_\_0\_\_ConnectionString="Server=prod-db;Database=testdb;Uid=prod-user;Pwd=prod-strong-pwd;"

export DbDynamicApi\_\_Security\_\_AppKey="prod-strong-app-key-123456"

export DbDynamicApi\_\_Cache\_\_DefaultType="Redis"

export DbDynamicApi\_\_Cache\_\_RedisConnectionString="prod-redis:6379,password=prod-redis-pwd,defaultDatabase=0"
```



1. 启动服务，环境变量将自动覆盖`appsettings.json`中的对应配置，优先以环境变量的配置为准。

### ➕ 插件扩展开发

项目支持自定义插件扩展能力，开发流程如下：



1. 实现基础插件接口 `IApiPlugin`，自定义参数处理、响应格式转换、数据权限校验等逻辑。

2. 插件支持的扩展点：

* `IParameterTransformPlugin`：自定义请求参数处理逻辑，例如解密、格式转换、自动添加租户 ID。

* `IResponseWrapPlugin`：自定义全局响应格式转换，按照业务前端要求统一封装返回结果。

* `IDataSourceProviderPlugin`：新增自定义数据源驱动，对接非标准关系型数据库。

* `IPermissionValidatePlugin`：自定义权限校验逻辑，对接公司统一的身份认证系统。

1. 将插件编译为`dll`文件，放置在项目根目录下的`plugins`文件夹中。

2. 启动服务，插件将自动加载生效。

## 🚀 部署指南

### 🐳 Docker 部署

项目根目录下提供标准`Dockerfile`、`docker-compose.yml`配置文件，部署流程如下：



1. 构建 Docker 镜像：



```
docker build -t dbdynamicapi:latest .
```



1. 启动容器：



```
docker run -d -p 5000:5000 --name dbdynamicapi \\

 -e DbDynamicApi\_\_DataSources\_\_0\_\_ConnectionString="Server=localhost;Database=testdb;Uid=root;Pwd=123456;" \\

 -e DbDynamicApi\_\_Security\_\_AppKey="your-valid-app-key" \\

 dbdynamicapi:latest
```

### 📦 Kubernetes 部署

提供标准`deployment.yaml`、`service.yaml`配置示例，部署流程如下：



1. 创建 ConfigMap 存储全局配置：



```
kubectl create configmap dbdynamicapi-config --fromfile=appsettings.json
```



1. 创建 Secret 存储敏感配置：



```
kubectl create secret generic dbdynamicapi-secret \\

 \--fromliteral=db-connection-string="Server=prod-db;Database=testdb;Uid=prod-user;Pwd=prod-strong-pwd;" \\

 \--fromliteral=app-key="prod-strong-app-key-123456"
```



1. 应用 Deployment、Service 配置：



```
kubectl apply -f deployment.yaml

kubectl apply -f service.yaml
```

### 🖥️ 其他部署方式

#### 1. Windows 服务部署

使用`sc`命令或`nssm`工具将程序注册为 Windows 服务，设置开机启动、服务异常自动重启。

#### 2. Linux 守护进程部署

创建`systemd`服务配置文件，放置在`/etc/systemd/system/`目录下，设置开机启动、服务异常自动重启。

## ❓ 常见问题与故障排查

### Q1：接口调用报错 “数据库连接失败”

**排查步骤**：



1. 检查数据源连接字符串格式、数据库账号密码权限、数据库网络连通性，确保宿主机 / 容器可以正常访问数据库实例。

2. 检查数据库连接池配置，确认`MaxPoolSize`设置未超出数据库实例支持的最大连接数限制。

3. 检查数据库是否开启远程连接、对应端口是否开放，是否启用了 IP 白名单限制。

4. 查看服务日志，确认实际执行的连接字符串内容，是否存在格式错误。

### Q2：修改配置文件后未生效

**排查步骤**：



1. 确认热更新开关`HotReload.Enable`设置为`true`，且`NotifyUrl`配置正确，可被配置中心正常回调。

2. 检查配置文件的编码格式、JSON 语法是否正确，有无缺失逗号、非法字符、注释格式错误。

3. 查看服务日志，确认配置变更事件被正常捕获，是否存在配置校验失败的异常日志。

4. 手动调用热更新接口`http://localhost:5000/admin/reload`，携带正确的`ApiKey`触发配置刷新。

### Q3：接口返回性能较慢

**排查步骤**：



1. 检查对应接口的缓存配置，确认`Cache.Enable`设置为`true`，缓存 Key 设置合理，未导致缓存频繁失效。

2. 分析执行 SQL 的执行计划，确认 SQL 语句未涉及全表扫描、缺少必要索引、存在复杂子查询。

3. 查看服务日志，确认接口耗时主要在数据库执行、数据序列化、还是网络传输环节。

4. 检查数据库连接池使用率，确认连接池无长时间等待、连接泄漏的异常情况。

### Q4：接口返回 403 无权限错误

**排查步骤**：



1. 检查请求头中`Authorization: Bearer <AppKey>`携带的密钥值，与配置文件`Security.AppKey`设置的内容是否完全匹配。

2. 检查`Security.IpWhitelist`配置，确认客户端调用 IP 在白名单内，或白名单配置的 IP 段格式合法。

3. 检查对应接口的`ApiRules.Authorize`设置，确认是否开启了权限校验。

4. 查看服务日志，记录具体权限校验失败原因，如 IP 不在白名单、AppKey 不合法、接口无访问权限。

### Q5：动态 SQL 执行报错

**排查步骤**：



1. 检查 SQL 语法格式，兼容目标数据库的 SQL 方言，例如 MySQL 与 SQL Server 的函数、关键字差异。

2. 检查参数占位符格式（#{}/\${}），与请求参数名称、类型、数量完全匹配，无缺失参数或多余占位符。

3. 开启 SQL 日志，查看实际执行的 SQL 语句与参数值，直接在数据库客户端执行该 SQL，验证语法正确性。

4. 检查动态 SQL 标签的嵌套逻辑，`<where>`/`<if>`/`<foreach>`标签的开闭顺序、条件判断逻辑合法。

### Q6：容器内启动服务报错 “端口占用”

**排查步骤**：



1. 检查宿主机端口映射配置，`docker run -p`或`docker-compose.yml`中宿主机端口未被其他进程占用。

2. 检查容器内服务监听端口，设置为未被容器内其他进程占用的空闲端口。

3. 检查宿主机防火墙、安全组配置，允许外部访问映射的宿主机端口。

## 🛣️ 开发路线图

### 版本 1.0.x（当前稳定版本）



* 基础多数据源支持、标准 CRUD 接口生成、多配置源优先级治理。

* 基础安全防护：IP 白名单、AppKey 校验、SQL 注入防护、参数校验。

* 基础性能优化：连接池管理、内存缓存、分页优化、N+1 查询防御。

* 标准化 Swagger 文档、文件日志、控制台日志输出。

* 支持原生部署、Docker 部署、Docker Compose 部署。

### 版本 1.1.x（规划中）



* 新增分布式缓存支持（Redis），完善缓存预热、主动刷新、缓存穿透 / 击穿防御。

* 新增配置中心对接支持，兼容 Apollo、Nacos、Consul，实现集群级配置统一管理。

* 支持多 SQL 组合执行，一次请求可完成多表查询、多数据组装、事务执行。

* 新增接口级灰度发布、流量镜像、流量复制能力，配合网关实现金丝雀发布。

* 新增更多国产数据库驱动支持，人大金仓、瀚高、神通等国产数据库。

### 版本 1.2.x（规划中）



* 集成 SkyWalking、Jaeger 链路追踪，完善全链路监控、性能瓶颈分析。

* 新增接口级数据脱敏、水印、行级权限控制，实现多租户数据隔离。

* 支持多语言国际化返回格式，中英文报错信息、多语言字段映射。

* 提供可视化管理后台，支持 GUI 配置数据源、API 规则、缓存、权限、SQL 在线测试。

* 新增请求 / 响应数据加密、签名校验，增强敏感数据传输安全。

### 版本 2.0.x（长期规划）



* 重构插件扩展架构，支持无重启加载、卸载、升级插件，实现热插拔扩展。

* 新增分布式事务支持，对接 Seata、XA 事务模式，实现跨库数据一致性。

* 集成 OAuth2.0、OpenID Connect、CAS 身份认证，对接企业统一身份中心。

* 支持分表分库规则可视化配置，自动路由分库分表数据源。

* 提供多语言 SDK 自动生成能力，Java、C#、Python、Go 客户端代码生成。

## 📄 开源协议

本项目采用 [MIT](https://github.com/CreatorXuYuFei/DbDynamicApi/blob/main/LICENSE) 开源协议，允许个人、商业项目自由使用、修改、分发，无需额外授权。

## 🤝 贡献指南

我们非常欢迎社区贡献代码、提交问题、编写使用文档。贡献流程如下：



1. 搜索 [Issue 列表](https://github.com/CreatorXuYuFei/DbDynamicApi/issues)，确认没有重复问题后，使用自带的 Issue 模板提交 bug 报告、新功能建议、使用问题反馈。

2. Fork 本仓库到你的 GitHub 账号，然后克隆到本地，基于`develop`分支创建新的功能 / 修复分支。

3. 开发规范：遵循.NET 官方代码规范、Microsoft 框架设计建议，添加必要的 XML 注释、单元测试、日志处理，保证代码质量；编写对应功能的使用示例、更新文档说明。

4. 提交 PR：编写清晰的 PR 说明，关联对应的 Issue，提交到目标仓库的`develop`分支，等待代码审查与合并。

5. 补充说明：

* 新功能开发请先提交 Issue 讨论，确认设计思路与实现方案后再进行开发。

* 修复 bug 请提供复现步骤、截图、错误日志，验证修复效果。

* 新增数据源驱动、扩展能力请提供对应的单元测试、使用示例文档。

* 文档修改请保证内容准确、结构清晰、符合标准 Markdown 格式。

## 👨‍💻 相关项目

本项目基于以下自研开源组件构建，形成完整的企业级技术生态栈：



* [Enterprise.Configuration](https://github.com/CreatorXuYuFei/Enterprise.Configuration)：自研企业级配置管理组件，提供多配置源优先级管理、热更新监听、强类型配置绑定、配置校验，为 DbDynamicApi 提供完整配置治理能力。

* [CoreLogger](https://github.com/CreatorXuYuFei/CoreLogger)：自研高性能结构化日志组件，为 DbDynamicApi 提供全链路日志追踪、多目标输出、异步分片写入、链路追踪能力。

* [NewLife.XCode](https://github.com/NewLifeX/NewLife.XCode)：底层依赖的高性能.NET ORM 框架，提供数据库连接池、SQL 执行、缓存、分表分库、自动建模能力。

## 🙏 致谢

感谢以下开源项目的贡献与支撑，让 DbDynamicApi 可以快速落地并形成企业级能力：



* [NewLife.XCode](https://github.com/NewLifeX/NewLife.XCode) 提供成熟的高性能 ORM 能力，解决数据库连接池、缓存、分表分库的通用场景问题。

* [ASP.NET Core](https://github.com/dotnet/aspnetcore) 提供成熟的跨平台 Web 宿主框架。

* [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) 提供标准的 OpenAPI 文档生成能力。

* 所有使用本项目、提交 Issue、贡献代码的社区开发者。



***

> 如果您觉得本项目有价值，欢迎给我们 Star ⭐！您的支持是我们持续迭代的动力。

如有使用问题，可提交 [Issue](https://github.com/CreatorXuYuFei/DbDynamicApi/issues/new) 或通过邮件联系维护团队。
