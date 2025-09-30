#import <Foundation/Foundation.h>

const char* GetInfoPlistValue(const char* key) {
    @autoreleasepool {
        NSString *nsKey = [NSString stringWithUTF8String:key];
        id value = [[NSBundle mainBundle] objectForInfoDictionaryKey:nsKey];

        if (!value) {
            return strdup(""); // không có key -> trả về chuỗi rỗng
        }

        NSString *result = nil;

        if ([value isKindOfClass:[NSString class]]) {
            result = (NSString*)value;
        }
        else if ([value isKindOfClass:[NSNumber class]]) {
            // NSNumber có thể là int, float, bool
            const char *objCType = [(NSNumber*)value objCType];
            if (strcmp(objCType, @encode(BOOL)) == 0) {
                result = [(NSNumber*)value boolValue] ? @"true" : @"false";
            } else {
                result = [(NSNumber*)value stringValue];
            }
        }
        else if ([value isKindOfClass:[NSArray class]] ||
                 [value isKindOfClass:[NSDictionary class]]) {
            // convert sang JSON string
            NSData *jsonData = [NSJSONSerialization dataWithJSONObject:value options:0 error:nil];
            result = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        }
        else if ([value isKindOfClass:[NSDate class]]) {
            NSDateFormatter *formatter = [[NSDateFormatter alloc] init];
            [formatter setDateFormat:@"yyyy-MM-dd'T'HH:mm:ssZ"];
            result = [formatter stringFromDate:(NSDate*)value];
        }
        else if ([value isKindOfClass:[NSData class]]) {
            result = [(NSData*)value base64EncodedStringWithOptions:0];
        }
        else {
            result = [value description]; // fallback
        }

        return strdup([result UTF8String]);
    }
}
