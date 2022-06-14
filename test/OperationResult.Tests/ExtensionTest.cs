﻿using Meteors;
using Microsoft.AspNetCore.Mvc;
using OperationResult.Tests.Mocks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OperationResult.Tests
{
    public class ExtensionTest
    {
        public static IEnumerable<object[]> FactData =>
        new List<object[]>
        {
            new object[] {  OperationResultTypes.Exception},
            new object[] { OperationResultTypes.NotExist},
            new object[] {OperationResultTypes.Exist}, 
            new object[] {  OperationResultTypes.Unauthorized},
            new object[] { OperationResultTypes.Forbidden},
            new object[] { OperationResultTypes.Failed},
            new object[] {  OperationResultTypes.Success},
        };


        [Fact]
        public void ToOperationResult()
        {
            var data = new FooUser();
            var operation = data.ToOperationResult();


            OperationResult<FooUser> operation1 = new();
            operation1.Data = data;
            operation1.OperationResultType = OperationResultTypes.Success;

            Assert.True(operation.Equals(operation1));
        }


        [Theory]
        [MemberData(nameof(FactData))]
        public void ToJsonResult(OperationResultTypes type)
        {
            var operation = Seed.Create<FooUser>(type);
            var result = operation.ToJsonResult();

            Assert.Equal((int)type, result.StatusCode);

            if(type == OperationResultTypes.Success)
            {
                var data = result.Value as FooUser;
                Assert.NotNull(data);
            } else
           
            if (type == OperationResultTypes.Exception)
            {
                Assert.True(operation.HasException);
                Assert.Equal(Seed.ToFullException(operation.Exception), result.Value?.ToString());
            }
               else
            if (type != OperationResultTypes.Success && type != OperationResultTypes.Exception)
            {
                Assert.Equal(type.ToString(), result.Value?.ToString());
            }

        }


        [Theory]
        [MemberData(nameof(FactData))]
        public async Task ToJsonResultAsync(OperationResultTypes type)
        {
            var operationAsync = Task.FromResult(Seed.Create<FooUser>(type));
            var operation = await operationAsync;
            var result =await operationAsync.ToJsonResultAsync();

            Assert.Equal((int)type, result.StatusCode);

            if (type == OperationResultTypes.Success)
            {
                var data = result.Value as FooUser;
                Assert.NotNull(data);
            }
            else

            if (type == OperationResultTypes.Exception)
            {
                Assert.True(operation.HasException);
                Assert.Equal(Seed.ToFullException(operation.Exception), result.Value?.ToString());
            }
            else
            if (type != OperationResultTypes.Success && type != OperationResultTypes.Exception)
            {
                Assert.Equal(type.ToString(), result.Value?.ToString());
            }

        }



        [Theory]
        [MemberData(nameof(FactData))]
        public void ToJsonResult_IsBody(OperationResultTypes type)
        {
            var operation = Seed.Create<FooUser>(type);
            var result = operation.ToJsonResult(true);

            Assert.Equal((int)type, result.StatusCode);

            OperationResult<FooUser>  bodyResult = result.Value as OperationResult<FooUser>;
            Assert.NotNull(bodyResult);

            if (type == OperationResultTypes.Success)
            {
                var data = bodyResult.Data;
                Assert.NotNull(data);
            }
            else

            if (type == OperationResultTypes.Exception)
            {
                Assert.True(operation.HasException);
                Assert.Equal(Seed.ToFullException(operation.Exception), bodyResult?.FullExceptionMessage);
            }
            else
            if (type != OperationResultTypes.Success && type != OperationResultTypes.Exception)
            {
                Assert.Equal(type.ToString(), bodyResult.Message.ToString());
            }

        }


        [Theory]
        [MemberData(nameof(FactData))]
        public async Task ToJsonResultAsync_IsBody(OperationResultTypes type)
        {
            var operationAsync = Task.FromResult(Seed.Create<FooUser>(type));
            var operation = await operationAsync;
            var result =await operationAsync.ToJsonResultAsync(true);

            Assert.Equal((int)type, result.StatusCode);

            OperationResult<FooUser> bodyResult = result.Value as OperationResult<FooUser>;
            Assert.NotNull(bodyResult);

            if (type == OperationResultTypes.Success)
            {
                var data = bodyResult.Data;
                Assert.NotNull(data);
            }
            else

            if (type == OperationResultTypes.Exception)
            {
                Assert.True(operation.HasException);
                Assert.Equal(Seed.ToFullException(operation.Exception), bodyResult?.FullExceptionMessage);
            }
            else
            if (type != OperationResultTypes.Success && type != OperationResultTypes.Exception)
            {
                Assert.Equal(type.ToString(), bodyResult.Message.ToString());
            }

        }





        [Theory]
        [MemberData(nameof(FactData))]
        public void WithStatusCode(OperationResultTypes type)
        {
            var operation = Seed.Create<FooUser>(type);
            var result = operation.WithStatusCode(507);

            Assert.Equal(507, result.StatusCode);
        }


        [Theory]
        [MemberData(nameof(FactData))]
        public async Task WithStatusCodeAsync(OperationResultTypes type)
        {
            var operation = Task.FromResult(Seed.Create<FooUser>(type));
            var result =await operation.WithStatusCodeAsync(507);

            Assert.Equal(507, result.StatusCode);
        }

    }
}